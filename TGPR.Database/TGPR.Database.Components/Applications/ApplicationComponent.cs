using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Applications;

namespace TGPR.Database.Components.Applications
{
    public interface IApplicationComponent
    {
        Task<IEnumerable<ApplicationModel>> GetApplicationsAsync();
    }

    internal class ApplicationComponent : IApplicationComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationModel>> GetApplicationsAsync()
        {
            using (var repo = _repoFactory.Create<IApplicationRepository>())
            {
                IEnumerable<Application> applications = await repo.GetAllAsync();

                IEnumerable<ApplicationModel> models = applications
                    .Select(_mapper.Map<ApplicationModel>)
                    .ToList();

                return models;
            }
        }
    }
}
