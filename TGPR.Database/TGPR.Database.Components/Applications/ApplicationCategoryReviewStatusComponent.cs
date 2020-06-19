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
    public interface IApplicationCategoryReviewStatusComponent
    {
        Task<IEnumerable<ApplicationCategoryReviewStatusModel>> GetStatusesAsync();
    }

    internal class ApplicationCategoryReviewStatusComponent : IApplicationCategoryReviewStatusComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public ApplicationCategoryReviewStatusComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicationCategoryReviewStatusModel>> GetStatusesAsync()
        {
            using (var repo = _repoFactory.Create<IApplicationCategoryReviewStatusRepository>())
            {
                IEnumerable<ApplicationCategoryReviewStatus> statuses = await repo.GetAllAsync();

                List<ApplicationCategoryReviewStatusModel> models = statuses
                    .Select(_mapper.Map<ApplicationCategoryReviewStatusModel>)
                    .ToList();

                return models;
            }
        }
    }
}
