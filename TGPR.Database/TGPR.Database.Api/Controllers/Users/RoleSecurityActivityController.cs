using System;
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
    [Authorize]
    public class RoleSecurityActivityController : ControllerBase
    {
        private readonly IRoleSecurityActivityComponent _roleSecurityActivityComponent;
        private readonly ILogger _logger;

        public RoleSecurityActivityController(IRoleSecurityActivityComponent roleSecurityActivityComponent, ILogger<RoleSecurityActivityController> logger)
        {
            _roleSecurityActivityComponent = roleSecurityActivityComponent;
            _logger = logger;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleWrite)]
        [Route("add")]
        public async Task<IActionResult> AddAsync(RoleSecurityActivityModel roleSecurityActivity)
        {
            try
            {
                RoleSecurityActivityModel activity = await _roleSecurityActivityComponent.AddSecurityActivityAsync(roleSecurityActivity);

                return Ok(activity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleWrite)]
        [Route("{id:guid}/remove")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            try
            {
                await _roleSecurityActivityComponent.RemoveSecurityActivityAsync(id);

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