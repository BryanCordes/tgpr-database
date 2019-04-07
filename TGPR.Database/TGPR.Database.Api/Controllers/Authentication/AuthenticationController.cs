using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TGPR.Database.Common.Models.Authenitcation;
using TGPR.Database.Components.Authenitcation;

namespace TGPR.Database.Api.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationComponent _authComponent;
        private readonly ILogger _logger;

        public AuthenticationController(IAuthenticationComponent authComponent, ILogger<AuthenticationController> logger)
        {
            _authComponent = authComponent;
            _logger = logger;
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel login)
        {
            try
            {
                string client = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                JwtResponseModel jwt = await _authComponent.LoginAsync(login, client);

                string token = Serialize(jwt);

                return Ok(token);
            }
            catch (InvalidCredentialException)
            {
                var loginError = new LoginErrorModel
                {
                    Error = "invalid_credentials"
                };

                return Ok(loginError);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                var loginError = new LoginErrorModel
                {
                    Error = "unexpected_error"
                };

                return BadRequest(loginError);
            }
        }

        [Route("refresh")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenModel refreshToken)
        {
            try
            {
                string client = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                JwtResponseModel jwt = await _authComponent.RefreshAsync(refreshToken.RefreshToken, refreshToken.Token, client);

                string token = Serialize(jwt);

                return Ok(token);
            }
            catch (InvalidCredentialException)
            {
                var loginError = new LoginErrorModel
                {
                    Error = "invalid_credentials"
                };
                return BadRequest(loginError);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);

                var loginError = new LoginErrorModel
                {
                    Error = "unexpected_error"
                };

                return BadRequest(loginError);
            }
        }

        private string Serialize(JwtResponseModel jwt)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            string token = JsonConvert.SerializeObject(jwt, settings);

            return token;
        }
    }
}