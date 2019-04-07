using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using TGPR.Database.Common.Data;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;
using TGPR.Database.DataAccess.Operational;
using TGPR.Database.DataAccess.Repositories.Users;
using TGPR.Database.Encryption;

namespace TGPR.Database.Components.Users
{
    public interface IUserComponent
    {
        Task<DataSourceResponse<UserSummaryModel>> GetAllAsync(DataSourceFilter filter);
        Task<UserModel> GetAsync(Guid userId);
        Task<UserModel> CreateAsync(UserModel user);
        Task UpdateAsync(Guid userId, UserModel user);
        Task<UserModel> GetLoginUserAsync(Expression<Func<User, bool>> predicate);
        Task UpdateLastLogin(Guid userId);
        Task<DateTime> InactivateAsync(Guid userId);
        Task ActivateAsync(Guid userId);
    }

    internal class UserComponent : IUserComponent
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IHashProvider _hashProvider;
        private readonly IMapper _mapper;

        public UserComponent(IRepositoryFactory repoFactory, IHashProvider hashProvider, IMapper mapper)
        {
            _repoFactory = repoFactory;
            _hashProvider = hashProvider;
            _mapper = mapper;
        }

        public async Task<DataSourceResponse<UserSummaryModel>> GetAllAsync(DataSourceFilter filter)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                if (string.IsNullOrWhiteSpace(filter.SortColumn))
                {
                    filter.SortColumn = nameof(User.Email);
                }

                DataSourceResponse<User> users = await repo.GetAsync(filter);

                List<UserSummaryModel> models = users
                    .Data
                    .Select(_mapper.Map<UserSummaryModel>)
                    .ToList();

                var response = new DataSourceResponse<UserSummaryModel>
                {
                    DataSourceFilter = users.DataSourceFilter,
                    TotalRecords = users.TotalRecords,
                    Data = models
                };

                return response;
            }
        }

        public async Task<UserModel> GetAsync(Guid userId)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                User entity = await repo.FindByAsync(x => x.UserId == userId, x => x.Roles);

                var model = _mapper.Map<UserModel>(entity);

                return model;
            }
        }

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                user.PasswordHash = _hashProvider.Hash(user.PasswordHash);

                var entity = _mapper.Map<User>(user);

                entity = await repo.AddAsync(entity);

                repo.SaveChanges();

                Guid userId = entity.UserId;

                entity = await repo.FindByAsync(x => x.UserId == userId, x => x.Roles);

                var savedUser = _mapper.Map<UserModel>(entity);

                return savedUser;
            }
        }

        public async Task UpdateAsync(Guid userId, UserModel user)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                User entity = await repo.FindByAsync(x => x.UserId == user.UserId, x => x.Roles);
                if (entity == null
                    || entity.UserId != userId)
                {
                    return;
                }

                entity.Email = user.Email;
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Address = user.Address;
                entity.PhoneNumber = user.PhoneNumber;

                DeleteUserRoles(entity, user);
                AddUserRoles(entity, user);

                entity.ObjectState = ObjectState.Modified;

                repo.ApplyStateChanges();

                await repo.UpdateAsync(entity);
            }
        }

        public async Task<DateTime> InactivateAsync(Guid userId)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                User entity = await repo.FindByAsync(x => x.UserId == userId);
                if (entity == null)
                {
                    return DateTime.MinValue;
                }

                entity.InactiveOn = DateTime.UtcNow;

                await repo.UpdateAsync(entity);

                return (DateTime)entity.InactiveOn;
            }
        }

        public async Task ActivateAsync(Guid userId)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                User entity = await repo.FindByAsync(x => x.UserId == userId);

                entity.InactiveOn = null;

                await repo.UpdateAsync(entity);
            }
        }

        private void DeleteUserRoles(User user, UserModel userModel)
        {
            IEnumerable<Guid> userRoleIds = userModel.Roles
                .Select(x => x.UserRoleId);

            var hs = new HashSet<Guid>(userRoleIds);

            IEnumerable<UserRole> deletedRoles = user.Roles
                .Where(x => !hs.Contains(x.UserRoleId));

            foreach (var deletedRole in deletedRoles)
            {
                deletedRole.ObjectState = ObjectState.Deleted;
            }
        }

        private void AddUserRoles(User user, UserModel userModel)
        {
            IEnumerable<Guid> userRoleIds = user.Roles
                .Select(x => x.UserRoleId);

            var hs = new HashSet<Guid>(userRoleIds);

            IEnumerable<UserRoleModel> addedRoles = userModel.Roles
                .Where(x => !hs.Contains(x.UserRoleId));

            foreach (var addedRole in addedRoles)
            {
                var userRole = new UserRole
                {
                    UserId = addedRole.UserId,
                    RoleId = addedRole.RoleId
                };

                user.Roles.Add(userRole);

                userRole.ObjectState = ObjectState.Added;
            }
        }

        public async Task<UserModel> GetLoginUserAsync(Expression<Func<User, bool>> predicate)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                User user = await repo.GetLoginUserAsync(predicate);

                var model = _mapper.Map<UserModel>(user);

                return model;
            }
        }

        public async Task UpdateLastLogin(Guid userId)
        {
            using (var repo = _repoFactory.Create<IUserRepository>())
            {
                User user = await repo.FindByAsync(x => x.UserId == userId);

                user.LastLoginOn = DateTime.UtcNow;

                await repo.UpdateAsync(user);
            }
        }
    }
}
