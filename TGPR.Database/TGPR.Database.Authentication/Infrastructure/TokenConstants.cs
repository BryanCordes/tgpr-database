using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TGPR.Database.Authentication.Infrastructure
{
    public static class TokenConstants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol";
                public const string Id = "id";
                public const string Activity = "activity";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        public static class Keys
        {
            private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
            public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}
