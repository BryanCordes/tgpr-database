using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGPR.Database.Authentication.Attibutes;
using TGPR.Database.Common.Enums.Security;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Applications;

namespace TGPR.Database.Api.Controllers.Applications
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ApplicationQuestionTypeController : ControllerBase
    {
        private readonly IApplicationQuestionTypeComponent _questionTypeComponent;

        public ApplicationQuestionTypeController(IApplicationQuestionTypeComponent questionTypeComponent)
        {
            _questionTypeComponent = questionTypeComponent;
        }

        [HttpGet]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateRead)]
        public async Task<IEnumerable<ApplicationQuestionTypeModel>> Get()
        {
            return await _questionTypeComponent.GetApplicationQuestionTypesAsync();
        }
    }
}