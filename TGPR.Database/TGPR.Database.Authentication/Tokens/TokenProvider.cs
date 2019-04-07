using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TGPR.Database.Authentication.Infrastructure;
using TGPR.Database.Common.Models.Authenitcation;
using TGPR.Database.Common.Models.Users;

namespace TGPR.Database.Authentication.Tokens
{
    public interface ITokenProvider
    {
        Task<JwtResponseModel> GenerateAsync(UserModel user);
    }

    internal class TokenProvider : ITokenProvider
    {
        private readonly TokenOptions _tokenOptions;

        public TokenProvider(IOptions<TokenOptions> tokenOptions)
        {
            _tokenOptions = tokenOptions.Value;

            ThrowIfInvalidOptions(_tokenOptions);
        }

        public async Task<JwtResponseModel> GenerateAsync(UserModel user)
        {
            string authToken = await GenerateTokenAsync(user);

            string userId = user.UserId.ToString();
            string refreshToken = await GenerateRefreshTokenAsync();

            DateTime date = DateTime.Now.AddTicks(_tokenOptions.ValidFor.Ticks);

            var response = new JwtResponseModel
            {
                Id = userId,
                AuthToken = authToken,
                RefreshToken = refreshToken,
                Expiry = ToUnixEpochDate(date)
            };

            return response;
        }

        private async Task<string> GenerateTokenAsync(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, await _tokenOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_tokenOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                new Claim(TokenConstants.Strings.JwtClaimIdentifiers.Id, user.UserId.ToString()),
                new Claim(TokenConstants.Strings.JwtClaimIdentifiers.Rol, TokenConstants.Strings.JwtClaims.ApiAccess)
            };

            IEnumerable<Claim> securityClaims = user.Roles
                .Select(x => x.Role)
                .SelectMany(x => x.SecurityActivities)
                .Select(x => x.SecurityActivityId)
                .Distinct()
                .Select(x => new Claim(TokenConstants.Strings.JwtClaimIdentifiers.Activity, x.ToString()));

            claims.AddRange(securityClaims);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: claims,
                notBefore: _tokenOptions.NotBefore,
                expires: _tokenOptions.Expiration,
                signingCredentials: _tokenOptions.SigningCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            string encodedJwt = tokenHandler.WriteToken(jwt);

            return encodedJwt;
        }

        private Task<string> GenerateRefreshTokenAsync()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);

                return Task.Run(() => refreshToken);
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        private static void ThrowIfInvalidOptions(TokenOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenOptions.JtiGenerator));
            }
        }
    }
}
