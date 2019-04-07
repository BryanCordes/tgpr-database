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
    public class ApplicationOptionController : ControllerBase
    {
        private readonly IApplicationOptionComponent _optionComponent;
        private readonly IApplicationUserHelper _userHelper;

        public ApplicationOptionController(IApplicationOptionComponent optionComponent, IApplicationUserHelper userHelper)
        {
            _optionComponent = optionComponent;
            _userHelper = userHelper;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("create")]
        public async Task<ApplicationOptionModel> PostAsync([FromBody]ApplicationOptionModel option)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            ApplicationOptionModel createdOption = await _optionComponent.CreateAsync(option, userId);

            return createdOption;
        }

        [HttpPut]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task<ApplicationOptionModel> PutAsync(int id, [FromBody] ApplicationOptionModel option)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            ApplicationOptionModel updatedOption = await _optionComponent.UpdateAsync(option, userId);

            return updatedOption;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("updateSortOrder")]
        public async Task UpdateSortOrderAsync([FromBody]ApplicationOptionModel option)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            await _optionComponent.UpdateSortOrderAsync(option, userId);
        }

        [HttpDelete]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            await _optionComponent.DeleteAsync(id, userId);
        }
    }
}