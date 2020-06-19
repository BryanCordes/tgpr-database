using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Entities.Applications;
using TGPR.Database.DataAccess.Infrastructure;

namespace TGPR.Database.DataAccess.Repositories.Applications
{
    public interface IApplicationRepository : IRepositoryAsync<Application>, IDataSourceRepositoryAsync<Application>
    {
        Task<Application> GetApplicationAsync(int applicationId);
    }

    internal class ApplicationRepository : DataSourceRepositoryBase<Application>, IApplicationRepository
    {
        public ApplicationRepository(TgprContext context) 
            : base(context)
        { }

        public async Task<Application> GetApplicationAsync(int applicationId)
        {
            Application application = await DbSet
                .Where(x => x.ApplicationId == applicationId)
                .Include(x => x.QuestionAnswers)
                .Include(x => x.ApplicationCategoryReviews)
                .FirstOrDefaultAsync();

            return application;
        }

        protected override IEnumerable<Expression<Func<Application, string>>> FilterProperties()
        {
            var props = new Expression<Func<Application, string>>[]
            {
                x => x.FirstName,
                x => x.LastName,
                x => x.Email,
                x => x.Address,
                x => x.City,
                x => x.State,
                x => x.Zip,
                x => x.Phone
            };

            return props;
        }
    }
}
