using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationRepository : IRepositoryAsync<Application>
    { }

    internal class ApplicationRepository : RepositoryBase<Application>
    {
        public ApplicationRepository(TgprContext context) 
            : base(context)
        { }
    }
}
