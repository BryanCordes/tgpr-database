using System.Threading.Tasks;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationCategoryRepository : IRepositoryAsync<ApplicationCategory>
    { }

    internal class ApplicationCategoryRepository : RepositoryBase<ApplicationCategory>, IApplicationCategoryRepository
    {
        public ApplicationCategoryRepository(TgprContext context) 
            : base(context)
        { }

        public override async Task DeleteAsync(ApplicationCategory entity)
        {
            entity.Deleted = true;

            await UpdateAsync(entity);
        }
    }
}
