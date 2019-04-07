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
    public class ApplicationCategoryController : ControllerBase
    {
        private readonly IApplicationCategoryComponent _categoryComponent;
        private readonly IApplicationUserHelper _userHelper;

        public ApplicationCategoryController(IApplicationCategoryComponent categoryComponent, IApplicationUserHelper userHelper)
        {
            _categoryComponent = categoryComponent;
            _userHelper = userHelper;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateRead)]
        [Route("create")]
        public async Task<ApplicationCategoryModel> PostAsync([FromBody]ApplicationCategoryModel category)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            ApplicationCategoryModel createdCategory = await _categoryComponent.CreateAsync(category, userId);

            return createdCategory;
        }

        [HttpPut]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task<ApplicationCategoryModel> PutAsync(int id, [FromBody] ApplicationCategoryModel category)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            ApplicationCategoryModel updatedCategory = await _categoryComponent.UpdateAsync(category, userId);

            return updatedCategory;
        }

        [HttpPost]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("updateSortOrder")]
        public async Task UpdateSortOrderAsync([FromBody]ApplicationCategoryModel category)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            await _categoryComponent.UpdateSortOrderAsync(category, userId);
        }

        [HttpDelete]
        [SecurityActivityRequired(SecurityActivityEnum.ApplicationTemplateWrite)]
        [Route("{id}")]
        public async Task DeleteAsync(int id)
        {
            string userId = _userHelper.GetCurrentUserId(User);

            await _categoryComponent.DeleteAsync(id, userId);
        }
    }
}