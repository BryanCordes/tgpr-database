using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TGPR.Database.Api.Helpers;
using TGPR.Database.Authentication.Attibutes;
using TGPR.Database.Common.Enums.Security;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Applications;

namespace TGPR.Database.Api.Controllers.Applications
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ApplicationQuestionController : ControllerBase
    {
        private readonly IApplicationQuestionComponent _questionComponent;
        private readonly IApplicationUserHelper _userHelper;

        public ApplicationQuestionController(IApplicationQuestionComponent questionComponent, IApplicationUserHelper userHelper)
        {
            _questionComponent = questionComponent;
            _userHelper = userHelper;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("create")]
        public async Task<ApplicationQuestionModel> PostAsync([FromBody]ApplicationQuestionModel question)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            ApplicationQuestionModel createdQuestion = await _questionComponent.CreateAsync(question, userId);

            return createdQuestion;
        }

        [HttpPut]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task<ApplicationQuestionModel> PutAsync(int id, [FromBody] ApplicationQuestionModel question)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            ApplicationQuestionModel updatedQuestion = await _questionComponent.UpdateAsync(question, userId);

            return updatedQuestion;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("updateSortOrder")]
        public async Task UpdateSortOrderAsync([FromBody]ApplicationQuestionModel question)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            await _questionComponent.UpdateSortOrderAsync(question, userId);
        }

        [HttpDelete]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            await _questionComponent.DeleteAsync(id, userId);
        }
    }
}