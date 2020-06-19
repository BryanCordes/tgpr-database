using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationCategoryReviewStatusRepository : IReadonlyRepositoryAsync<ApplicationCategoryReviewStatus>
    { }


    internal class ApplicationCategoryReviewStatusRepository : RepositoryBase<ApplicationCategoryReviewStatus>, IApplicationCategoryReviewStatusRepository
    {
        public ApplicationCategoryReviewStatusRepository(TgprContext context) 
            : base(context)
        { }
    }
}
