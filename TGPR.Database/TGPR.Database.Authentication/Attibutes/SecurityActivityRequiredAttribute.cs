using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TGPR.Database.Authentication.Infrastructure;
using TGPR.Database.Common.Enums.Security;

namespace TGPR.Database.Authentication.Attibutes
{
    public class SecurityActivityRequiredAttribute : TypeFilterAttribute
    {
        public SecurityActivityRequiredAttribute(SecurityActivityEnum securityActivity)
            :base(typeof(ClaimRequirementFilter))
        {
            int activity = (int) securityActivity;
            string value = activity.ToString();

            var claim = new Claim(TokenConstants.Strings.JwtClaimIdentifiers.Activity, value);

            Arguments = new object[] { claim };
        }
    }

    internal class ClaimRequirementFilter : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasSecurityActivity = context.HttpContext.User.Claims
                .Any(x => x.Type == TokenConstants.Strings.JwtClaimIdentifiers.Activity
                          && x.Value == _claim.Value);

            if (hasSecurityActivity)
            {
                return;
            }

            context.Result = new ForbidResult();
        }
    }
}
