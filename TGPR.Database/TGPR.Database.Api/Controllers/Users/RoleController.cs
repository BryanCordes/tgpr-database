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
    [Authorize]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleComponent _roleComponent;
        private readonly ILogger _logger;

        public RoleController(IRoleComponent roleComponent, ILogger<RoleController> logger)
        {
            _roleComponent = roleComponent;
            _logger = logger;
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleRead)]
        public async Task<IActionResult> GetRolesAsync()
        {
            try
            {
                IEnumerable<RoleModel> roles = await _roleComponent.GetRolesAsync();

                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleRead)]
        [Route("users")]
        public async Task<IActionResult> GetEditableRolesAsync()
        {
            try
            {
                IEnumerable<RoleModel> roles = await _roleComponent.GetEditableRolesAsync();

                return Ok(roles);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleWrite)]
        public async Task<IActionResult> Create(RoleModel role)
        {
            try
            {
                RoleModel savedRole = await _roleComponent.CreateAsync(role);

                return Ok(savedRole);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleWrite)]
        public async Task<IActionResult> Update(RoleModel role)
        {
            try
            {
                await _roleComponent.UpdateAsync(role);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpDelete]
        [SecurityActivityRequired(SecurityActivityEnum.UserRoleWrite)]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool deleted = await _roleComponent.DeleteAsnyc(id);

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, ex.Message);

                return BadRequest();
            }
        }
    }
}