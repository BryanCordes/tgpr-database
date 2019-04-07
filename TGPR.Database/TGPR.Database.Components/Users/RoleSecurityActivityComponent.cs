using System;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Users;

namespace TGPR.Database.Components.Users
{
    public interface IRoleSecurityActivityComponent
    {
        Task<RoleSecurityActivityModel> AddSecurityActivityAsync(RoleSecurityActivityModel roleSecurityActivity);
        Task RemoveSecurityActivityAsync(Guid roleSecurityActivityId);
    }

    internal class RoleSecurityActivityComponent : IRoleSecurityActivityComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public RoleSecurityActivityComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<RoleSecurityActivityModel> AddSecurityActivityAsync(RoleSecurityActivityModel roleSecurityActivity)
        {
            using (var repo = _repoFactory.Create<IRoleSecurityActivityRepository>())
            {
                var entity = _mapper.Map<RoleSecurityActivity>(roleSecurityActivity);

                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                var model = _mapper.Map<RoleSecurityActivityModel>(entity);

                return model;
            }
        }

        public async Task RemoveSecurityActivityAsync(Guid roleSecurityActivityId)
        {
            using (var repo = _repoFactory.Create<IRoleSecurityActivityRepository>())
            {
                RoleSecurityActivity entity = await repo.FindByAsync(x => x.RoleSecurityActivityId == roleSecurityActivityId);
                if (entity == null)
                {
                    return;
                }

                await repo.DeleteAsync(entity);
            }
        }
    }
}
