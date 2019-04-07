using System;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Users;

namespace TGPR.Database.Components.Users
{
    public interface IUserRoleComponent
    {
        Task<UserRoleModel> AddUserRoleAsync(UserRoleModel userRole);
        Task RemoveUserRoleAsync(Guid userRoleId);
    }

    internal class UserRoleComponent : IUserRoleComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public UserRoleComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<UserRoleModel> AddUserRoleAsync(UserRoleModel userRole)
        {
            using (var repo = _repoFactory.Create<IUserRoleRepository>())
            {
                var entity = _mapper.Map<UserRole>(userRole);

                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                var model = _mapper.Map<UserRoleModel>(entity);

                return model;
            }
        }

        public async Task RemoveUserRoleAsync(Guid userRoleId)
        {
            using (var repo = _repoFactory.Create<IUserRoleRepository>())
            {
                UserRole entity = await repo.FindByAsync(x => x.UserRoleId == userRoleId);
                if (entity == null)
                {
                    return;
                }

                await repo.DeleteAsync(entity);
            }
        }
    }
}
