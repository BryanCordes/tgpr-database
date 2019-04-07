using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Users;

namespace TGPR.Database.Components.Users
{
    public interface ISecurityActivityComponent
    {
        Task<IEnumerable<SecurityActivityModel>> GetAllAsync();
    }

    internal class SecurityActivityComponent : ISecurityActivityComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public SecurityActivityComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SecurityActivityModel>> GetAllAsync()
        {
            using (var repo = _repoFactory.Create<ISecurityActivityRepository>())
            {
                IEnumerable<SecurityActivity> entities = await repo.GetAllAsync();

                List<SecurityActivityModel> models = entities
                    .Select(_mapper.Map<SecurityActivityModel>)
                    .ToList();

                return models;
            }
        }
    }
}
