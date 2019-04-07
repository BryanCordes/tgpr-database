using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Users
{
    public interface IRoleSecurityActivityRepository : IRepositoryAsync<RoleSecurityActivity>
    { }

    internal class RoleSecurityActivityRepository : RepositoryBase<RoleSecurityActivity>, IRoleSecurityActivityRepository
    {
        public RoleSecurityActivityRepository(TgprContext context) 
            : base(context)
        { }
    }
}
