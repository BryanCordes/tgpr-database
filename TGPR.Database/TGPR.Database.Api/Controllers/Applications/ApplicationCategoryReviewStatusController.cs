using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Applications;

namespace TGPR.Database.Api.Controllers.Applications
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ApplicationCategoryReviewStatusController : ControllerBase
    {
        private readonly IApplicationCategoryReviewStatusComponent _statusComponent;

        public ApplicationCategoryReviewStatusController(IApplicationCategoryReviewStatusComponent statusComponent)
        {
            _statusComponent = statusComponent;
        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationCategoryReviewStatusModel>> GetAsync()
        {
            return await _statusComponent.GetStatusesAsync();
        }
    }
}