using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationQuestionTypeRepository : IReadonlyRepositoryAsync<ApplicationQuestionType>
    { }

    internal class ApplicationQuestionTypeRepository : RepositoryBase<ApplicationQuestionType>, IApplicationQuestionTypeRepository
    {
        public ApplicationQuestionTypeRepository(TgprContext context) 
            : base(context)
        { }
    }
}
