using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Users
{
    public interface ISecurityActivityRepository : IReadonlyRepositoryAsync<SecurityActivity>
    { }

    internal class SecurityActivityRepository : RepositoryBase<SecurityActivity>, ISecurityActivityRepository
    {
        public SecurityActivityRepository(TgprContext context) 
            : base(context)
        { }
    }
}
