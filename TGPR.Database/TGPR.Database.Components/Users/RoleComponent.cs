using System;
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
    public interface IRoleComponent
    {
        Task<IEnumerable<RoleModel>> GetRolesAsync();
        Task<IEnumerable<RoleModel>> GetEditableRolesAsync();
        Task<RoleModel> CreateAsync(RoleModel role);
        Task UpdateAsync(RoleModel role);
        Task<bool> DeleteAsnyc(Guid roleId);
    }

    internal class RoleComponent : IRoleComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public RoleComponent(IRepositoryFactory repoFactory, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> GetRolesAsync()
        {
            using (var repo = _repoFactory.Create<IRoleRepository>())
            {
                IEnumerable<Role> roles = await repo.GetAllAsync();

                List<RoleModel> models = roles
                    .Select(_mapper.Map<RoleModel>)
                    .ToList();

                return models;
            }
        }

        public async Task<IEnumerable<RoleModel>> GetEditableRolesAsync()
        {
            using (var repo = _repoFactory.Create<IRoleRepository>())
            {
                IEnumerable<Role> roles = await repo.GetEditableRolesAsync();

                List<RoleModel> models = roles
                    .Select(_mapper.Map<RoleModel>)
                    .ToList();

                return models;
            }
        }

        public async Task<RoleModel> CreateAsync(RoleModel role)
        {
            using (var repo = _repoFactory.Create<IRoleRepository>())
            {
                var entity = _mapper.Map<Role>(role);

                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                var model = _mapper.Map<RoleModel>(entity);

                return model;
            }
        }

        public async Task UpdateAsync(RoleModel role)
        {
            using (var repo = _repoFactory.Create<IRoleRepository>())
            {
                Role entity = await repo.FindByAsync(x => x.RoleId == role.RoleId);
                if (entity == null)
                {
                    return;
                }

                entity.Name = role.Name;

                await repo.UpdateAsync(entity);
            }
        }

        public async Task<bool> DeleteAsnyc(Guid roleId)
        {
            try
            {
                using (var repo = _repoFactory.Create<IRoleRepository>())
                using(var roleSecurityActivityRepo = _repoFactory.Create<IRoleSecurityActivityRepository>())
                {
                    Role entity = await repo.FindByAsync(x => x.RoleId == roleId);
                    if (entity == null)
                    {
                        return false;
                    }

                    IEnumerable<RoleSecurityActivity> securityActivities = await roleSecurityActivityRepo
                        .SearchAsync(x => x.RoleId == entity.RoleId);
                    foreach (RoleSecurityActivity securityActivity in securityActivities)
                    {
                        await roleSecurityActivityRepo.DeleteAsync(securityActivity);
                    }

                    roleSecurityActivityRepo.SaveChanges();

                    await repo.DeleteAsync(entity);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
