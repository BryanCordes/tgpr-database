using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Helpers;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationCategoryComponent
    {
        Task<ApplicationCategoryModel> CreateAsync(ApplicationCategoryModel applicationCategory, string userId);
        Task<ApplicationCategoryModel> UpdateAsync(ApplicationCategoryModel applicationCategory, string userId);
        Task UpdateSortOrderAsync(ApplicationCategoryModel applicationCategory, string userId);
        Task UpdateReviewSortOrderAsync(ApplicationCategoryModel applicationCategory, string userId);
        Task DeleteAsync(int applicationCategoryId, string userId);
    }

    internal class ApplicationCategoryComponent : IApplicationCategoryComponent
    {
        private readonly IApplicationTemplateComponent _templateComponent;
        private readonly IRepositoryFactory _repoFactory;
        private readonly ISortingHelper _sortingHelper;
        private readonly IMapper _mapper;

        public ApplicationCategoryComponent(IApplicationTemplateComponent templateComponent, IRepositoryFactory repoFactory, ISortingHelper sortingHelper, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _templateComponent = templateComponent;
            _mapper = mapper;
            _sortingHelper = sortingHelper;
        }

        public async Task<ApplicationCategoryModel> CreateAsync(ApplicationCategoryModel applicationCategory, string userId)
        {
            var entity = _mapper.Map<ApplicationCategory>(applicationCategory);
            using (var repo = _repoFactory.Create<IApplicationCategoryRepository>())
            
            {
                entity = await repo.AddAsync(entity);
            }

            await _templateComponent.SetModifiedAsync(entity.ApplicationTemplateId, userId);

            var model = _mapper.Map<ApplicationCategoryModel>(entity);

            return model;
        }

        public async Task<ApplicationCategoryModel> UpdateAsync(ApplicationCategoryModel applicationCategory, string userId)
        {
            ApplicationCategory entity;

            using (var repo = _repoFactory.Create<IApplicationCategoryRepository>())
            {
                entity = await repo.GetByIdAsync(applicationCategory.ApplicationCategoryId);
                if (entity == null)
                {
                    return null;
                }

                entity.Name = applicationCategory.Name;
                entity.HasReview = applicationCategory.HasReview;

                if (entity.ReviewerSortOrder != applicationCategory.ReviewerSortOrder)
                {
                    IEnumerable<ApplicationCategory> all = await repo
                        .SearchAsync(x => x.ApplicationTemplateId == entity.ApplicationTemplateId
                                          && x.ApplicationCategoryId != entity.ApplicationCategoryId);


                    all = _sortingHelper.ReorderReviewer(all, entity.ReviewerSortOrder, applicationCategory.ReviewerSortOrder);
                    foreach (var updatedEntity in all)
                    {
                        await repo.UpdateAsync(updatedEntity);
                    }

                    entity.ReviewerSortOrder = applicationCategory.ReviewerSortOrder;
                }

                await repo.UpdateAsync(entity);

                await _templateComponent.SetModifiedAsync(entity.ApplicationTemplateId, userId);
            }

            var model = _mapper.Map<ApplicationCategoryModel>(entity);

            return model;
        }

        public async Task UpdateSortOrderAsync(ApplicationCategoryModel applicationCategory, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationCategoryRepository>())
            {
                ApplicationCategory entity = await repo.GetByIdAsync(applicationCategory.ApplicationCategoryId);

                if (entity == null)
                {
                    return;
                }

                ApplicationCategory replacedEntity = await repo.FindByAsync(x => x.ApplicationSortOrder == applicationCategory.ApplicationSortOrder
                                                                                 && x.ApplicationTemplateId == applicationCategory.ApplicationTemplateId);
                if (replacedEntity != null)
                {
                    replacedEntity.ApplicationSortOrder = entity.ApplicationSortOrder;

                    await repo.UpdateAsync(replacedEntity);
                }

                entity.ApplicationSortOrder = applicationCategory.ApplicationSortOrder;

                await repo.UpdateAsync(entity);

                await _templateComponent.SetModifiedAsync(entity.ApplicationTemplateId, userId);
            }
        }

        public async Task UpdateReviewSortOrderAsync(ApplicationCategoryModel applicationCategory, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationCategoryRepository>())
            {
                ApplicationCategory entity = await repo.GetByIdAsync(applicationCategory.ApplicationCategoryId);

                IEnumerable<ApplicationCategory> all = await repo
                    .SearchAsync(x => x.ApplicationTemplateId == entity.ApplicationTemplateId
                                      && x.ApplicationCategoryId != entity.ApplicationCategoryId);


                all = _sortingHelper.ReorderReviewer(all, entity.ReviewerSortOrder, applicationCategory.ReviewerSortOrder);
                foreach (var updatedEntity in all)
                {
                    await repo.UpdateAsync(updatedEntity);
                }

                entity.ReviewerSortOrder = applicationCategory.ReviewerSortOrder;

                await repo.UpdateAsync(entity);

                await _templateComponent.SetModifiedAsync(entity.ApplicationTemplateId, userId);
            }
        }

        public async Task DeleteAsync(int applicationCategoryId, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationCategoryRepository>())
            {
                ApplicationCategory entity = await repo.GetByIdAsync(applicationCategoryId);

                await repo.DeleteAsync(entity);

                repo.SaveChanges();

                IEnumerable<ApplicationCategory> all = await repo
                    .SearchAsync(x => x.ApplicationTemplateId == entity.ApplicationTemplateId
                                      && x.ApplicationCategoryId != entity.ApplicationCategoryId);

                List<ApplicationCategory> categoryList = all.ToList();

                // bump application sort order
                foreach (ApplicationCategory categoryEntity in categoryList.Where(x => x.ApplicationSortOrder > entity.ApplicationSortOrder))
                {
                    categoryEntity.ApplicationSortOrder--;
                }

                // bump reviewer sort order
                foreach (ApplicationCategory categoryEntity in categoryList.Where(x => x.ReviewerSortOrder > entity.ReviewerSortOrder))
                {
                    categoryEntity.ReviewerSortOrder--;
                }

                foreach (var categoryEntity in categoryList)
                {
                    await repo.UpdateAsync(categoryEntity);
                }

                await _templateComponent.SetModifiedAsync(entity.ApplicationTemplateId, userId);
            }
        }
    }
}
