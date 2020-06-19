using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGPR.Database.Authentication.Attibutes;
using TGPR.Database.Common.Data;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Enums.Security;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Applications;

namespace TGPR.Database.Api.Controllers.Applications
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationComponent _applicationComponent;
        private readonly ILogger _logger;

        public ApplicationController(IApplicationComponent applicationComponent, ILogger<ApplicationController> logger)
        {
            _applicationComponent = applicationComponent;
            _logger = logger;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.CompanionApplicationRead)]
        [Route("companion")]
        public async Task<IActionResult> GetCompanionApplicationsAsync([FromBody]DataSourceFilter filter)
        {
            try
            {
                DataSourceResponse<ApplicationModel> applications = await _applicationComponent.GetApplicationsAsync(filter, ApplicationTypeEnum.Companion);

                return Ok(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("create")]
        public async Task<IActionResult> PostAsync([FromBody]ApplicationModel application)
        {
            try
            {
                ApplicationModel createdApplication = await _applicationComponent.CreateApplicationAsync(application);

                return Ok(createdApplication);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}