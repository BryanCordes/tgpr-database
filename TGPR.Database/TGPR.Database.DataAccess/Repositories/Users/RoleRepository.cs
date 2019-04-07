using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Users;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Users
{
    public interface IRoleRepository : IRepositoryAsync<Role>
    {
        Task<IEnumerable<Role>> GetEditableRolesAsync();
    }

    internal class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(TgprContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Role>> GetEditableRolesAsync()
        {
            var superUserId = Guid.Parse("CEBD2B84-DAFA-46EF-B54E-887D7FE2FCCE");

            List<Role> editableRoles = await DbSet
                .Where(x => x.RoleId != superUserId)
                .Include(x => x.SecurityActivities)
                .ToListAsync();

            return editableRoles;
        }
    }
}
