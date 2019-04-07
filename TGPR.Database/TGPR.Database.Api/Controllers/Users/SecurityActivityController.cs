using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGPR.Database.Authentication.Attibutes;
using TGPR.Database.Common.Enums;
using TGPR.Database.Common.Enums.Security;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.Components.Users;

namespace TGPR.Database.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityActivityController : ControllerBase
    {
        private readonly ISecurityActivityComponent _securityActivityComponent;
        private readonly ILogger _logger;

        public SecurityActivityController(ISecurityActivityComponent securityActivityComponent, ILogger<SecurityActivityController> logger)
        {
            _securityActivityComponent = securityActivityComponent;
            _logger = logger;
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleRead)]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<SecurityActivityModel> activities = await _securityActivityComponent.GetAllAsync();

                return Ok(activities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}