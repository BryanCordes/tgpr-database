using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TGPR.Database.Authentication.Attibutes;
using TGPR.Database.Common.Data;
using TGPR.Database.Common.Enums;
using TGPR.Database.Common.Enums.Security;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.Components.Users;

namespace TGPR.Database.Api.Controllers.Users
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserComponent _userComponent;
        private readonly ILogger _logger;

        public UserController(IUserComponent userComponent, ILogger<UserController> logger)
        {
            _userComponent = userComponent;
            _logger = logger;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserRead)]
        [Route("summary")]
        public async Task<IActionResult> GetAllAsync(DataSourceFilter filter)
        {
            try
            {
                DataSourceResponse<UserSummaryModel> users = await _userComponent.GetAllAsync(filter);

                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.UserRead)]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                UserModel user = await _userComponent.GetAsync(id);

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserWrite)]
        public async Task<IActionResult> CreateAsync(UserModel user)
        {
            try
            {
                UserModel savedUser = await _userComponent.CreateAsync(user);

                return Ok(savedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPut]
        [SecurityActivityRequired(SecurityActivityEnum.UserWrite)]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UserModel user)
        {
            try
            {
                await _userComponent.UpdateAsync(id, user);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserWrite)]
        [Route("{id:guid}/inactivate")]
        public async Task<IActionResult> InactivateAsync(Guid id)
        {
            try
            {
                DateTime inactiveDate = await _userComponent.InactivateAsync(id);

                return Ok(inactiveDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return BadRequest();
            }
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.UserWrite)]
        [Route("{id:guid}/activate")]
        public async Task<IActionResult> ActivateAsync(Guid id)
        {
            try
            {
                await _userComponent.ActivateAsync(id);

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