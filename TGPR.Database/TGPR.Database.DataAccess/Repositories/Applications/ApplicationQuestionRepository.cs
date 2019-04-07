using System.Threading.Tasks;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationQuestionRepository : IRepositoryAsync<ApplicationQuestion>
    { }

    internal class ApplicationQuestionRepository : RepositoryBase<ApplicationQuestion>, IApplicationQuestionRepository
    {
        public ApplicationQuestionRepository(TgprContext context) 
            : base(context)
        { }

        // This is a soft delete
        public override async Task DeleteAsync(ApplicationQuestion entity)
        {
            entity.Deleted = true;

            await UpdateAsync(entity);
        }
    }
}
