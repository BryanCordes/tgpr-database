using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Data;
using TGPR.Database.Common.Enums.Applications;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationComponent
    {
        Task<DataSourceResponse<ApplicationModel>> GetApplicationsAsync(DataSourceFilter filter, ApplicationTypeEnum applicationType);
        Task<ApplicationModel> GetApplicationAsync(int applicationId);
        Task<ApplicationModel> CreateApplicationAsync(ApplicationModel application);
    }

    internal class ApplicationComponent : IApplicationComponent
    {
        private readonly IApplicationTemplateComponent _templateComponent;
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationComponent(IApplicationTemplateComponent templateComponent, IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
            _templateComponent = templateComponent;
        }

        public async Task<DataSourceResponse<ApplicationModel>> GetApplicationsAsync(DataSourceFilter filter, ApplicationTypeEnum applicationType)
        {
            using (var repo = _repoFactory.Create<IApplicationRepository>())
            {
                DataSourceResponse<Application> applications = await repo.GetAsync(filter, x => x.ApplicationTemplate.ApplicationTypeId == (int) applicationType);
                
                List<ApplicationModel> models = applications
                        .Data
                        .Select(_mapper.Map<ApplicationModel>)
                        .ToList();

                var response = new DataSourceResponse<ApplicationModel>
                {
                    DataSourceFilter = applications.DataSourceFilter,
                    TotalRecords = applications.TotalRecords,
                    Data = models
                };

                return response;
            }
        }

        public async Task<ApplicationModel> GetApplicationAsync(int applicationId)
        {
            using (var repo = _repoFactory.Create<IApplicationRepository>())
            {
                Application application = await repo.GetApplicationAsync(applicationId);

                var model = _mapper.Map<ApplicationModel>(application);

                ApplicationTemplateModel template = await _templateComponent.GetReviewTemplateAsync(model.ApplicationTemplateId);

                model.ApplicationTemplate = template;

                return model;
            }
        }

        public async Task<ApplicationModel> CreateApplicationAsync(ApplicationModel application)
        {
            using (var repo = _repoFactory.Create<IApplicationRepository>())
            {
                var entity = _mapper.Map<Application>(application);

                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                var model = _mapper.Map<ApplicationModel>(entity);

                return model;
            }
        }
    }
}
