using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Users
{
    public interface IUserRoleRepository : IRepositoryAsync<UserRole>
    { }

    internal class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(TgprContext context) 
            : base(context)
        { }
    }
}
