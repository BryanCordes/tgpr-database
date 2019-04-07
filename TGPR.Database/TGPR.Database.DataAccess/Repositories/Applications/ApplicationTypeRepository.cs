using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationTypeRepository : IReadonlyRepositoryAsync<ApplicationType>
    { }

    internal class ApplicationTypeRepository : RepositoryBase<ApplicationType>, IApplicationTypeRepository
    {
        public ApplicationTypeRepository(TgprContext context) 
            : base(context)
        { }
    }
}
