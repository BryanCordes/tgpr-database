using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGPR.Database.Api.Helpers;
using TGPR.Database.Authentication.Attibutes;
using TGPR.Database.Common.Data;
using TGPR.Database.Common.Enums.Security;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Applications;

namespace TGPR.Database.Api.Controllers.Applications
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ApplicationTemplateController : ControllerBase
    {
        private readonly IApplicationTemplateComponent _applicationTemplateComponent;
        private readonly IApplicationUserHelper _userHelper;
        private readonly ILogger _logger;

        public ApplicationTemplateController(IApplicationTemplateComponent applicationTemplateComponent, IApplicationUserHelper userHelper, ILogger<ApplicationTemplateController> logger)
        {
            _applicationTemplateComponent = applicationTemplateComponent;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateRead)]
        [Route("{id}")]
        public async Task<IActionResult> GetTemplateAsync(int id)
        {
            try
            {
                ApplicationTemplateModel template = await _applicationTemplateComponent.GetTemplateAsync(id);

                return Ok(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateRead)]
        [Route("summary")]
        public async Task<IActionResult> GetTemplatesAsync(DataSourceFilter filter)
        {
            try
            {
                DataSourceResponse<ApplicationTemplateModel> templates = await _applicationTemplateComponent.GetTemplatesAsync(filter);

                return Ok(templates);
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
        public async Task<IActionResult> PostAsync([FromBody]ApplicationTemplateModel template)
        {
            try
            {
                string userId = _userHelper.GetCurrentUserId(User);

                ApplicationTemplateModel createdTemplate = await _applicationTemplateComponent.CreateTemplateAsync(template, userId);

                return Ok(createdTemplate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("name/{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] ApplicationTemplateModel template)
        {
            try
            {
                string userId = _userHelper.GetCurrentUserId(User);

                await _applicationTemplateComponent.UpdateName(id, template, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("active/{id}")]
        public async Task<IActionResult> ActiveAsync(int id)
        {
            try
            {
                string userId = _userHelper.GetCurrentUserId(User);

                await _applicationTemplateComponent.SetActiveAsync(id, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                string userId = _userHelper.GetCurrentUserId(User);

                await _applicationTemplateComponent.DeleteAsync(id, userId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}