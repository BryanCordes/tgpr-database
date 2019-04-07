using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using TGPR.Database.Authentication.Infrastructure;

namespace TGPR.Database.Api.Helpers
{
    public interface IApplicationUserHelper
    {
        string GetCurrentUserId(ClaimsPrincipal user);
    }

    internal class ApplicationUserHelper : IApplicationUserHelper
    { 
        public string GetCurrentUserId(ClaimsPrincipal user)
        {
            var claim = user.Claims
                .FirstOrDefault(x => x.Type == TokenConstants.Strings.JwtClaimIdentifiers.Id);

            if (string.IsNullOrWhiteSpace(claim?.Value))
            {
                throw new AuthenticationException();
            }

            return claim.Value;
        }
    }
}
