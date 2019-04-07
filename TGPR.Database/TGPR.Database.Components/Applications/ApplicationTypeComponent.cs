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
    public interface IApplicationTypeComponent
    {
        Task<IEnumerable<ApplicationTypeModel>> GetApplicationTypesAsync();
    }

    internal class ApplicationTypeComponent : IApplicationTypeComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationTypeComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationTypeModel>> GetApplicationTypesAsync()
        {
            using (var repo = _repoFactory.Create<IApplicationTypeRepository>())
            {
                IEnumerable<ApplicationType> types = await repo.GetAllAsync();

                List<ApplicationTypeModel> models = types
                    .Select(_mapper.Map<ApplicationTypeModel>)
                    .ToList();

                return models;
            }
        }
    }
}
