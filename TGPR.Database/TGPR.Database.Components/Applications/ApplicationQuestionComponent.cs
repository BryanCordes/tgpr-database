using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Enums;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Helpers;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationQuestionComponent
    {
        Task<ApplicationQuestionModel> CreateAsync(ApplicationQuestionModel question, string userId);
        Task<ApplicationQuestionModel> UpdateAsync(ApplicationQuestionModel question, string userId);
        Task UpdateSortOrderAsync(ApplicationQuestionModel question, string userId);
        Task DeleteAsync(int questionId, string userId);
    }

    internal class ApplicationQuestionComponent : IApplicationQuestionComponent
    {
        private readonly IApplicationTemplateComponent _templateComponent;
        private readonly IApplicationOptionComponent _optionComponent;
        private readonly IRepositoryFactory _repoFactory;
        private readonly ISortingHelper _sortingHelper;
        private readonly IMapper _mapper;

        public ApplicationQuestionComponent(IApplicationTemplateComponent templateComponent, IApplicationOptionComponent optionComponent, IRepositoryFactory repoFactory, IMapper mapper, ISortingHelper sortingHelper)
        {
            _templateComponent = templateComponent;
            _repoFactory = repoFactory;
            _mapper = mapper;
            _sortingHelper = sortingHelper;
            _optionComponent = optionComponent;
        }

        public async Task<ApplicationQuestionModel> CreateAsync(ApplicationQuestionModel question, string userId)
        {
            var entity = _mapper.Map<ApplicationQuestion>(question);

            IEnumerable<ApplicationOptionModel> newOptions;

            using (var repo = _repoFactory.Create<IApplicationQuestionRepository>())
            {
                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                newOptions = await _optionComponent.CreateOptionsAsync(entity.ApplicationQuestionId, (ApplicationQuestionTypeEnum)entity.ApplicationQuestionTypeId);

                int templateId = await repo.GetValueAsync(x => x.ApplicationQuestionId == entity.ApplicationQuestionId, x => x.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(templateId, userId);
            }

            var model = _mapper.Map<ApplicationQuestionModel>(entity);

            model.Options = newOptions.ToList();

            return model;
        }

        public async Task<ApplicationQuestionModel> UpdateAsync(ApplicationQuestionModel question, string userId)
        {
            ApplicationQuestion entity;
            IEnumerable<ApplicationOptionModel> newOptions;

            using (var repo = _repoFactory.Create<IApplicationQuestionRepository>())
            {
                entity = await repo.FindByAsync(x => x.ApplicationQuestionId == question.ApplicationQuestionId, x => x.Options);
                if (entity == null)
                {
                    return null;
                }

                entity.Text = question.Text;
                entity.Width = question.Width;

                newOptions = await TryUpdateApplicationQuestionType(entity, question);

                await TryUpdateReviewerSortOrder(repo, entity, question);

                await repo.UpdateAsync(entity);

                int applicationTemplateId = await repo.GetValueAsync(
                    x => x.ApplicationQuestionId == question.ApplicationQuestionId,
                    x => x.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(applicationTemplateId, userId);
            }

            var model = _mapper.Map<ApplicationQuestionModel>(entity);
            if (newOptions.Any())
            {
                model.Options = newOptions.ToList();
            }

            model.Options = model.Options
                .Where(x => !x.Deleted)
                .ToList();

            return model;
        }

        public async Task UpdateSortOrderAsync(ApplicationQuestionModel question, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationQuestionRepository>())
            {
                ApplicationQuestion entity = await repo.GetByIdAsync(question.ApplicationQuestionId);
                if (entity == null)
                {
                    return;
                }

                ApplicationQuestion replacedEntity;

                if (entity.ParentApplicationOptionId == null)
                {
                    replacedEntity = await repo.FindByAsync(x => x.ApplicationSortOrder == question.ApplicationSortOrder
                                                                 && x.ApplicationCategoryId == question.ApplicationCategoryId
                                                                 && x.ParentApplicationOptionId == null);
                }
                else
                {
                    replacedEntity = await repo.FindByAsync(x => x.ApplicationSortOrder == question.ApplicationSortOrder
                                                                 && x.ParentApplicationOptionId == question.ParentApplicationOptionId);
                }
                    
                if (replacedEntity != null)
                {
                    replacedEntity.ApplicationSortOrder = entity.ApplicationSortOrder;

                    await repo.UpdateAsync(replacedEntity);
                }

                entity.ApplicationSortOrder = question.ApplicationSortOrder;

                await repo.UpdateAsync(entity);

                int applicationTemplateId = await repo.GetValueAsync(
                    x => x.ApplicationQuestionId == question.ApplicationQuestionId,
                    x => x.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(applicationTemplateId, userId);
            }
        }

        public async Task DeleteAsync(int questionId, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationQuestionRepository>())
            {
                ApplicationQuestion entity = await repo.GetByIdAsync(questionId);
                if (entity == null)
                {
                    return;
                }

                await repo.DeleteAsync(entity);

                IEnumerable<ApplicationQuestion> all;
                if (entity.ParentApplicationOptionId == null)
                {
                    all = await repo
                        .SearchAsync(x => x.ApplicationCategoryId == entity.ApplicationCategoryId
                                          && x.ApplicationQuestionId != entity.ApplicationQuestionId
                                          && !x.Deleted);
                }
                else
                {
                    all = await repo
                        .SearchAsync(x => x.ParentApplicationOptionId == entity.ParentApplicationOptionId
                                          && x.ApplicationQuestionId != entity.ApplicationQuestionId
                                          && !x.Deleted);
                }

                List<ApplicationQuestion> questionList = all.ToList();

                // bump application sort order
                foreach (ApplicationQuestion questionEntity in questionList.Where(x => x.ApplicationSortOrder > entity.ApplicationSortOrder))
                {
                    questionEntity.ApplicationSortOrder--;
                }

                // bump reviewer sort order
                foreach (ApplicationQuestion questionEntity in questionList.Where(x => x.ReviewerSortOrder > entity.ReviewerSortOrder))
                {
                    questionEntity.ReviewerSortOrder--;
                }

                foreach (var questionEntity in questionList)
                {
                    await repo.UpdateAsync(questionEntity);
                }

                int applicationTemplateId = await repo.GetValueAsync(
                    x => x.ApplicationQuestionId == questionId,
                    x => x.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(applicationTemplateId, userId);
            }
        }

        private async Task<IEnumerable<ApplicationOptionModel>> TryUpdateApplicationQuestionType(ApplicationQuestion entity, ApplicationQuestionModel question)
        {
            if (entity.ApplicationQuestionTypeId == question.ApplicationQuestionTypeId)
            {
                List<ApplicationOptionModel> options = entity.Options
                    .Select(_mapper.Map<ApplicationOptionModel>)
                    .ToList();

                return options;
            }

            entity.ApplicationQuestionTypeId = question.ApplicationQuestionTypeId;

            await _optionComponent.DeleteOptionsAsync(entity.ApplicationQuestionId);

            return await _optionComponent.CreateOptionsAsync(entity.ApplicationQuestionId, (ApplicationQuestionTypeEnum) entity.ApplicationQuestionTypeId);
        }

        private async Task TryUpdateReviewerSortOrder(IApplicationQuestionRepository repo, ApplicationQuestion entity, ApplicationQuestionModel question)
        {
            if (entity.ReviewerSortOrder == question.ReviewerSortOrder)
            {
                return;
            }

            IEnumerable<ApplicationQuestion> all;
            if (entity.ParentApplicationOptionId == null)
            {
                all = await repo
                    .SearchAsync(x => x.ApplicationCategoryId == entity.ApplicationCategoryId
                                      && x.ApplicationQuestionId != entity.ApplicationQuestionId
                                      && !x.Deleted);
            }
            else
            {
                all = await repo
                    .SearchAsync(x => x.ParentApplicationOptionId == entity.ParentApplicationOptionId
                                      && x.ApplicationQuestionId != entity.ApplicationQuestionId
                                      && !x.Deleted);
            }

            all = _sortingHelper.ReorderReviewer(all, entity.ReviewerSortOrder, question.ReviewerSortOrder);
            foreach (var updatedEntity in all)
            {
                await repo.UpdateAsync(updatedEntity);
            }

            entity.ReviewerSortOrder = question.ReviewerSortOrder;
        }
    }
}
