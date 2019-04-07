using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.Components.Applications.QuestionTypeOptions;
using TGPR.Database.Components.Helpers;
using TGPR.Database.Components.Infrastructure;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationOptionComponent
    {
        Task<ApplicationOptionModel> CreateAsync(ApplicationOptionModel option, string userId);
        Task<IEnumerable<ApplicationOptionModel>> CreateOptionsAsync(int questionId, ApplicationQuestionTypeEnum questionType);
        Task<ApplicationOptionModel> UpdateAsync(ApplicationOptionModel option, string userId);
        Task UpdateSortOrderAsync(ApplicationOptionModel option, string userId);
        Task DeleteAsync(int optionId, string userId);
        Task DeleteOptionsAsync(int questionId);
    }

    internal class ApplicationOptionComponent : IApplicationOptionComponent
    {
        private readonly IApplicationTemplateComponent _templateComponent;
        private readonly IEnumerable<IQuestionTypeOptions> _questionTypeOptions;
        private readonly IRepositoryFactory _repoFactory;
        private readonly ISortingHelper _sortingHelper;
        private readonly IMapper _mapper;

        public ApplicationOptionComponent(IApplicationTemplateComponent templateComponent, IEnumerable<IQuestionTypeOptions> questionTypeOptions, IRepositoryFactory repoFactory, ISortingHelper sortingHelper, IMapper mapper)
        {
            _templateComponent = templateComponent;
            _repoFactory = repoFactory;
            _questionTypeOptions = questionTypeOptions;
            _sortingHelper = sortingHelper;
            _mapper = mapper;
        }

        public async Task<ApplicationOptionModel> CreateAsync(ApplicationOptionModel option, string userId)
        {
            var entity = _mapper.Map<ApplicationOption>(option);

            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                int templateId = await repo.GetValueAsync(
                    x => x.ApplicationOptionId == entity.ApplicationOptionId,
                    x => x.ApplicationQuestion.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(templateId, userId);
            }

            var model = _mapper.Map<ApplicationOptionModel>(entity);

            return model;
        }

        public async Task<IEnumerable<ApplicationOptionModel>> CreateOptionsAsync(int questionId, ApplicationQuestionTypeEnum questionType)
        {
            var questionOptions = _questionTypeOptions
                .First(x => x.QuestionType == questionType);

            return await questionOptions.GetOptionsAsync(questionId);
        }

        public async Task<ApplicationOptionModel> UpdateAsync(ApplicationOptionModel option, string userId)
        {
            ApplicationOption entity;

            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                entity = await repo.FindByAsync(x => x.ApplicationOptionId == option.ApplicationOptionId, x => x.ChildQuestions);
                if (entity == null)
                {
                    return null;
                }

                await TryUpdateReviewerSortOrder(repo, entity, option);

                entity.Text = option.Text;
                entity.ApplicationOptionStatusId = option.ApplicationOptionStatusId;

                await repo.UpdateAsync(entity);

                int templateId = await repo.GetValueAsync(
                    x => x.ApplicationOptionId == entity.ApplicationOptionId,
                    x => x.ApplicationQuestion.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(templateId, userId);
            }

            var model = _mapper.Map<ApplicationOptionModel>(entity);

            model.ChildQuestions = model.ChildQuestions
                ?.Where(x => !x.Deleted)
                .ToList();

            return model;
        }

        public async Task UpdateSortOrderAsync(ApplicationOptionModel option, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                ApplicationOption entity = await repo.GetByIdAsync(option.ApplicationOptionId);
                if (entity == null)
                {
                    return;
                }

                ApplicationOption replacedEntity = await repo.FindByAsync(x => x.ApplicationQuestionId == option.ApplicationQuestionId
                                                                               && x.ApplicationSortOrder == option.ApplicationSortOrder);
                if (replacedEntity != null)
                {
                    replacedEntity.ApplicationSortOrder = entity.ApplicationSortOrder;

                    await repo.UpdateAsync(replacedEntity);
                }

                entity.ApplicationSortOrder = option.ApplicationSortOrder;

                await repo.UpdateAsync(entity);

                int templateId = await repo.GetValueAsync(
                    x => x.ApplicationOptionId == entity.ApplicationOptionId,
                    x => x.ApplicationQuestion.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(templateId, userId);
            }
        }

        public async Task DeleteOptionsAsync(int questionId)
        {
            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                IEnumerable<ApplicationOption> options = await repo.SearchAsync(x => x.ApplicationQuestionId == questionId);
                foreach (var option in options)
                {
                    await repo.DeleteAsync(option);
                }
            }
        }

        public async Task DeleteAsync(int optionId, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationOptionRepository>())
            {
                ApplicationOption entity = await repo.GetByIdAsync(optionId);
                if (entity == null)
                {
                    return;
                }

                await repo.DeleteAsync(entity);

                int templateId = await repo.GetValueAsync(
                    x => x.ApplicationOptionId == optionId,
                    x => x.ApplicationQuestion.ApplicationCategory.ApplicationTemplateId);

                await _templateComponent.SetModifiedAsync(templateId, userId);
            }
        }

        private async Task TryUpdateReviewerSortOrder(IApplicationOptionRepository repo, ApplicationOption entity, ApplicationOptionModel option)
        {
            if (entity.ReviewerSortOrder == option.ReviewerSortOrder)
            {
                return;
            }

            IEnumerable<ApplicationOption> all = await repo
                    .SearchAsync(x => x.ApplicationQuestionId == entity.ApplicationQuestionId
                                      && x.ApplicationOptionId != entity.ApplicationOptionId
                                      && !x.Deleted);
    
            all = _sortingHelper.ReorderReviewer(all, entity.ReviewerSortOrder, option.ReviewerSortOrder);
            foreach (var updatedEntity in all)
            {
                await repo.UpdateAsync(updatedEntity);
            }

            entity.ReviewerSortOrder = option.ReviewerSortOrder;
        }
    }
}
