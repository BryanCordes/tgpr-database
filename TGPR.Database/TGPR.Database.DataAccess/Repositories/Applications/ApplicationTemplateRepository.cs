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
    public interface IApplicationTemplateRepository : IRepositoryAsync<ApplicationTemplate>, IDataSourceRepositoryAsync<ApplicationTemplate>
    {
        Task<ApplicationTemplate> GetTemplateAsync(int applicationTemplateId);
    }

    internal class ApplicationTemplateRepository : DataSourceRepositoryBase<ApplicationTemplate>, IApplicationTemplateRepository
    {
        public ApplicationTemplateRepository(TgprContext context) 
            : base(context)
        { }

        public async Task<ApplicationTemplate> GetTemplateAsync(int applicationTemplateId)
        {
            var query = await DbSet
                .Select(x => new
                {
                    Template = x,
                    Categories = x.Categories
                        .Where(c => !c.Deleted)
                        .Select(c => new
                        {
                            Category = c,
                            Questions = c.Questions
                                .Where(q => !q.Deleted)
                                .Select(q => new
                                {
                                    Question = q,
                                    Options = q.Options
                                        .Where(o => !o.Deleted)
                                        .OrderBy(o => o.ApplicationSortOrder)
                                        .ToList()
                                })
                                .OrderBy(q => q.Question.ApplicationSortOrder)
                                .ToList()
                        })
                        .OrderBy(c => c.Category.ApplicationSortOrder)
                        .ToList()
                })
                .FirstOrDefaultAsync(x => x.Template.ApplicationTemplateId == applicationTemplateId);

            query.Template.Categories = query.Categories
                .Select(x => x.Category)
                .ToList();

            foreach (var category in query.Categories)
            {
                category.Category.Questions = category.Questions
                    .Select(x => x.Question)
                    .ToList();

                foreach (var question in category.Questions)
                {
                    question.Question.Options = question.Options
                        .ToList();
                }
            }

            ApplicationTemplate template = query.Template;

            return template;
        }

        // This is a soft delete
        public override async Task DeleteAsync(ApplicationTemplate entity)
        {
            entity.Deleted = true;

            await UpdateAsync(entity);
        }

        protected override IEnumerable<Expression<Func<ApplicationTemplate, string>>> FilterProperties()
        {
            var props = new Expression<Func<ApplicationTemplate, string>>[]
            {
                x => x.Name
            };

            return props;
        }
    }
}
