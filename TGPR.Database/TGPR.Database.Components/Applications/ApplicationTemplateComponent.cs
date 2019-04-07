using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Data;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationTemplateComponent
    {
        Task<ApplicationTemplateModel> GetTemplateAsync(int applicationTemplateId);
        Task<DataSourceResponse<ApplicationTemplateModel>> GetTemplatesAsync(DataSourceFilter filter);
        Task<ApplicationTemplateModel> CreateTemplateAsync(ApplicationTemplateModel template, string userId);
        Task UpdateName(int applicationTemplateId, ApplicationTemplateModel template, string userId);
        Task SetActiveAsync(int applicationTemplateId, string userId);
        Task SetModifiedAsync(int applicationTemplateId, string userId);
        Task DeleteAsync(int applicationTemplateId, string userId);
    }

    internal class ApplicationTemplateComponent : IApplicationTemplateComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationTemplateComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<ApplicationTemplateModel> GetTemplateAsync(int applicationTemplateId)
        {
            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                ApplicationTemplate template = await repo.GetTemplateAsync(applicationTemplateId);

                repo.DiscardChanges();

                var model = _mapper.Map<ApplicationTemplateModel>(template);

                ArrangeQuestions(model);

                return model;
            }
        }

        private void ArrangeQuestions(ApplicationTemplateModel template)
        {
            foreach (var category in template.Categories)
            {
                Dictionary<int, List<ApplicationQuestionModel>> childQuestions = category.Questions
                    .Where(x => x.ParentApplicationOptionId != null)
                    .GroupBy(x => (int)x.ParentApplicationOptionId)
                    .ToDictionary(x => x.Key, x => x.ToList());

                if (childQuestions.Count == 0)
                {
                    continue;
                }

                IEnumerable<ApplicationOptionModel> options = category
                    .Questions
                    .SelectMany(x => x.Options)
                    .Where(x => childQuestions.ContainsKey(x.ApplicationOptionId));
                foreach (var option in options)
                {
                    option.ChildQuestions = childQuestions[option.ApplicationOptionId];
                }

                List<ApplicationQuestionModel> rootQuestions = category.Questions
                    .Where(x => x.ParentApplicationOptionId == null)
                    .ToList();

                category.Questions = rootQuestions;
            }
        }

        public async Task<DataSourceResponse<ApplicationTemplateModel>> GetTemplatesAsync(DataSourceFilter filter)
        {
            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                if (string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    filter.SortColumn = nameof(ApplicationTemplate.Name);
                }

                DataSourceResponse<ApplicationTemplate> templates = await repo.GetAsync(filter, x => !x.Deleted);

                List<ApplicationTemplateModel> models = templates
                    .Data
                    .Select(_mapper.Map<ApplicationTemplateModel>)
                    .ToList();

                var response = new DataSourceResponse<ApplicationTemplateModel>
                {
                    DataSourceFilter = templates.DataSourceFilter,
                    TotalRecords = templates.TotalRecords,
                    Data = models
                };

                return response;
            }
        }

        public async Task<ApplicationTemplateModel> CreateTemplateAsync(ApplicationTemplateModel template, string userId)
        {
            ApplicationTemplate entity = _mapper.Map<ApplicationTemplate>(template);

            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                Guid userUid = new Guid(userId);
                entity.CreatedById = userUid;

                entity.Active = false;

                entity = await repo.AddAsync(entity);
            }

            var model = _mapper.Map<ApplicationTemplateModel>(entity);

            return model;
        }

        public async Task UpdateName(int applicationTemplateId, ApplicationTemplateModel template, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                ApplicationTemplate entity = await repo.GetByIdAsync(applicationTemplateId);

                entity.Name = template.Name;
                entity.UpdatedOn = DateTime.UtcNow;

                Guid userUid = new Guid(userId);
                entity.UpdatedById = userUid;

                await repo.UpdateAsync(entity);
            }
        }

        public async Task SetActiveAsync(int applicationTemplateId, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                ApplicationTemplate entity = await repo.GetByIdAsync(applicationTemplateId);

                var selectedTypeTemplate = await repo.FindByAsync(x => x.ApplicationTypeId == entity.ApplicationTypeId
                                                                       && x.Active);
                Guid userUid = new Guid(userId);

                if (selectedTypeTemplate != null)
                {
                    selectedTypeTemplate.Active = false;
                    selectedTypeTemplate.UpdatedOn = DateTime.UtcNow;
                    selectedTypeTemplate.UpdatedById = userUid;

                    await repo.UpdateAsync(selectedTypeTemplate);
                }

                entity.Active = true;
                entity.UpdatedOn = DateTime.UtcNow;
                entity.UpdatedById = userUid;

                await repo.UpdateAsync(entity);
            }
        }

        public async Task SetModifiedAsync(int applicationTemplateId, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                ApplicationTemplate entity = await repo.GetByIdAsync(applicationTemplateId);

                entity.UpdatedOn = DateTime.UtcNow;

                Guid userUid = new Guid(userId);
                entity.UpdatedById = userUid;
            }
        }

        public async Task DeleteAsync(int applicationTemplateId, string userId)
        {
            using (var repo = _repoFactory.Create<IApplicationTemplateRepository>())
            {
                ApplicationTemplate entity = await repo.GetByIdAsync(applicationTemplateId);

                entity.UpdatedOn = DateTime.UtcNow;

                Guid userUid = new Guid(userId);
                entity.UpdatedById = userUid;


                await repo.DeleteAsync(entity);
            }
        }
    }
}
