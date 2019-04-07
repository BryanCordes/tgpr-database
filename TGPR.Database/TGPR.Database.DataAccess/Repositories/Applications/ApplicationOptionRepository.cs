using System.Threading.Tasks;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationOptionRepository : IRepositoryAsync<ApplicationOption>
    { }

    internal class ApplicationOptionRepository : RepositoryBase<ApplicationOption>, IApplicationOptionRepository
    {
        public ApplicationOptionRepository(TgprContext context) 
            : base(context)
        { }

        // This is a soft delete
        public override async Task DeleteAsync(ApplicationOption entity)
        {
            entity.Deleted = true;

            await UpdateAsync(entity);
        }
    }
}
