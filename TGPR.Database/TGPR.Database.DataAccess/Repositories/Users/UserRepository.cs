using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Users
{
    public interface IUserRepository : IRepositoryAsync<User>, IDataSourceRepositoryAsync<User>
    {
        Task<User> GetLoginUserAsync(Expression<Func<User, bool>> predicate);
    }

    internal class UserRepository : DataSourceRepositoryBase<User>, IUserRepository
    {
        public UserRepository(TgprContext context)
            : base(context)
        { }

        public async Task<User> GetLoginUserAsync(Expression<Func<User, bool>> predicate)
        {
            User user = await DbSet
                .Where(predicate)
                .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.SecurityActivities)
                .ThenInclude(x => x.SecurityActivity)
                .FirstOrDefaultAsync();

            return user;
        }

        protected override IEnumerable<Expression<Func<User, string>>> FilterProperties()
        {
            var props = new Expression<Func<User, string>>[]
            {
                x => x.Email,
                x => x.FirstName,
                x => x.LastName,
                x => x.Email,
                x => x.PhoneNumber
            };

            return props;
        }
    }
}
