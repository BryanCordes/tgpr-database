using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ApplicationTypeController : ControllerBase
    {
        private readonly IApplicationTypeComponent _applicationTypeComponent;

        public ApplicationTypeController(IApplicationTypeComponent applicationTypeComponent)
        {
            _applicationTypeComponent = applicationTypeComponent;
        }

        [HttpGet]
        public async Task<IEnumerable<ApplicationTypeModel>> Get()
        {
            try
            {
                return await _applicationTypeComponent.GetApplicationTypesAsync();
            }
            catch (Exception ex)
            {
                string message = ex.Message;

                return await Task.Run<IEnumerable<ApplicationTypeModel>>(() =>
                    Enumerable.Empty<ApplicationTypeModel>());
            }
        }
    }
}