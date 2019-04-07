using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TGPR.Database.Authentication.Infrastructure;
using TGPR.Database.Authentication.Tokens;
using TGPR.Database.Common.Models.Authenitcation;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.Components.Users;
using TGPR.Database.Encryption;

namespace TGPR.Database.Components.Authenitcation
{
    public interface IAuthenticationComponent
    {
        Task<JwtResponseModel> LoginAsync(LoginModel loginModel, string client);
        Task<JwtResponseModel> RefreshAsync(string refreshToken, string authToken, string client);
    }

    internal class AuthenticationComponent : IAuthenticationComponent
    {
        private readonly IUserComponent _userComponent;
        private readonly IUserRefreshTokenComponent _refreshTokenComponent;
        private readonly ITokenProvider _tokenProvider;
        private readonly IHashProvider _hashProvider;
        private readonly TokenValidationParameters _validationParameters;

        public AuthenticationComponent(IUserComponent userComponent, IUserRefreshTokenComponent refreshTokenComponent, ITokenProvider tokenProvider, IHashProvider hashProvider, TokenValidationParameters validationParameters)
        {
            _userComponent = userComponent;
            _refreshTokenComponent = refreshTokenComponent;
            _tokenProvider = tokenProvider;
            _hashProvider = hashProvider;
            _validationParameters = validationParameters;
        }

        public async Task<JwtResponseModel> LoginAsync(LoginModel loginModel, string client)
        {
            UserModel user = await _userComponent.GetLoginUserAsync(x => x.Email == loginModel.Email);
            if (user == null)
            {
                throw new InvalidCredentialException();
            }

            if (!_hashProvider.Verify(loginModel.Password, user.PasswordHash))
            {
                throw new InvalidCredentialException();
            }

            JwtResponseModel jwt = await _tokenProvider.GenerateAsync(user);

            await _refreshTokenComponent.AddUserRefreshTokenAsync(user.UserId.ToString(), jwt.RefreshToken, jwt.AuthToken, client);

            await _userComponent.UpdateLastLogin(user.UserId);

            return jwt;
        }

        public async Task<JwtResponseModel> RefreshAsync(string refreshToken, string authToken, string client)
        {
            bool isValid = await _refreshTokenComponent.HasUserRefreshTokenAsync(refreshToken, authToken, client);
            if (!isValid)
            {
                throw new SecurityTokenException("invalid_grant");
            }

            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(authToken);

            var identity = (ClaimsIdentity)principal.Identity;

            string userId = identity.Claims.Single(c => c.Type == TokenConstants.Strings.JwtClaimIdentifiers.Id).Value;

            if (Guid.TryParse(userId, out Guid userUid))
            {
                throw new SecurityTokenException("invalid_grant");
            }

            UserModel user = await _userComponent.GetLoginUserAsync(x => x.UserId == userUid);
            if (user == null)
            {
                throw new SecurityTokenException("invalid_grant");
            }

            JwtResponseModel jwt = await _tokenProvider.GenerateAsync(user);

            await _refreshTokenComponent.AddUserRefreshTokenAsync(user.UserId.ToString(), jwt.RefreshToken, jwt.AuthToken, client);

            return jwt;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            _validationParameters.ValidateLifetime = false;

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, _validationParameters, out var securityToken);
            if (!(securityToken is JwtSecurityToken jwtToken))
            {
                _validationParameters.ValidateLifetime = true;

                throw new SecurityTokenException("invalid_grant");
            }

            _validationParameters.ValidateLifetime = true;

            return principal;
        }
    }
}
