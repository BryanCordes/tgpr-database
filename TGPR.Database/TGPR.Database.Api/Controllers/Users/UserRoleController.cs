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
    [Authorize]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleComponent _userRoleComponent;
        private readonly ILogger _logger;

        public UserRoleController(IUserRoleComponent userRoleComponent, ILogger<UserRoleController> logger)
        {
            _userRoleComponent = userRoleComponent;
            _logger = logger;
        }


        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleWrite)]
        [Route("add")]
        public async Task<IActionResult> AddAsync(UserRoleModel userRole)
        {
            try
            {
                UserRoleModel addedUserRole = await _userRoleComponent.AddUserRoleAsync(userRole);

                return Ok(addedUserRole);
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
                await _userRoleComponent.RemoveUserRoleAsync(id);

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