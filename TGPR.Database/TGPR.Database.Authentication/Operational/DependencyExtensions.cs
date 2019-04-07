using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TGPR.Database.Authentication.Infrastructure;
using TGPR.Database.Authentication.Tokens;

namespace TGPR.Database.Authentication.Operational
{
    public static class DependencyExtensions
    {
        public static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();

            IConfigurationSection tokenConfig = configuration.GetSection(nameof(TokenOptions));

            services.Configure<TokenOptions>(options =>
            {
                options.Issuer = tokenConfig[nameof(TokenOptions.Issuer)];
                options.Audience = tokenConfig[nameof(TokenOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(TokenConstants.Keys.SigningKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,

                //ValidateIssuer = true,
                //ValidIssuer = tokenConfig[nameof(TokenOptions.Issuer)],

                ValidateAudience = false,

                //ValidateAudience = true,
                //ValidAudience = tokenConfig[nameof(TokenOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = TokenConstants.Keys.SigningKey
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = tokenConfig[nameof(TokenOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });


        }
    }
}
