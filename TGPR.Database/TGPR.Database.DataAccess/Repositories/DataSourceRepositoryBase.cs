using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TGPR.Database.Common.Data;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.DataAccess.Extensions;

namespace TGPR.Database.DataAccess.Repositories
{
    internal abstract class DataSourceRepositoryBase<TEntity> : RepositoryBase<TEntity>
        where TEntity : class
    {
        protected DataSourceRepositoryBase(TgprContext context)
            : base(context)
        { }

        protected abstract IEnumerable<Expression<Func<TEntity, string>>> FilterProperties();

        public virtual DataSourceResponse<TEntity> Get(DataSourceFilter filter, params Expression<Func<TEntity, bool>>[] additionalFilters)
        {
            IQueryable<TEntity> query = DbSet
                .AsQueryable();

            foreach (var additionalFilter in additionalFilters)
            {
                query = query.Where(additionalFilter);
            }

            int totalCount = query.Count();

            TryAddStringFilter(query, filter, out query);

            int skipCount = filter.Page * filter.PageSize;

            query = query
                .Skip(skipCount)
                .Take(filter.PageSize);

            if (filter.SortDirection == "desc")
            {
                query = query
                    .OrderByPropertyDescending(filter.SortColumn);
            }
            else
            {
                query = query
                    .OrderByProperty(filter.SortColumn);
            }

            List<TEntity> results = query
                .ToList();

            var response = new DataSourceResponse<TEntity>
            {
                DataSourceFilter = filter,
                TotalRecords = totalCount,
                Data = results
            };

            return response;
        }

        public virtual async Task<DataSourceResponse<TEntity>> GetAsync(DataSourceFilter filter, params Expression<Func<TEntity, bool>>[] additionalFilters)
        {
            IQueryable<TEntity> query = DbSet
                .AsQueryable();

            foreach (var additionalFilter in additionalFilters)
            {
                query = query.Where(additionalFilter);
            }

            int totalCount = await query.CountAsync();

            TryAddStringFilter(query, filter, out query);

            TryAddIntFilter(query, filter, out query);

            int skipCount = filter.Page * filter.PageSize;

            query = query
                .Skip(skipCount)
                .Take(filter.PageSize);

            if (filter.SortDirection == "desc")
            {
                query = query
                    .OrderByPropertyDescending(filter.SortColumn);
            }
            else
            {
                query = query
                    .OrderByProperty(filter.SortColumn);
            }
            
            List<TEntity> results = await query
                .ToListAsync();

            var response = new DataSourceResponse<TEntity>
            {
                DataSourceFilter = filter,
                TotalRecords = totalCount,
                Data = results
            };

            return response;
        }

        protected void TryAddIntFilter(IQueryable<TEntity> query, DataSourceFilter filter, out IQueryable<TEntity> filteredQuery)
        {
            filteredQuery = query;

            if (string.IsNullOrWhiteSpace(filter.Filter))
            {
                return;
            }

            int number;
            if (!int.TryParse(filter.Filter, out number))
            {
                return;
            }

            // get all int fields
            List<PropertyInfo> propInfos = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType == typeof(int))
                .ToList();

            if (propInfos.Count == 0)
            {
                return;
            }

            Expression<Func<TEntity, bool>> filterExpression = x => false;

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            ConstantExpression constant = Expression.Constant(number);

            foreach (PropertyInfo info in propInfos)
            {
                MemberExpression property = Expression.Property(parameter, info);
                BinaryExpression body = Expression.Equal(property, constant);

                filterExpression = Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Or(filterExpression.Body, body),
                    parameter);
            }

            filteredQuery = filteredQuery
                .Where(filterExpression);
        }

        protected void TryAddDateFilter(IQueryable<TEntity> query, DataSourceFilter filter, out IQueryable<TEntity> filteredQuery)
        {
            filteredQuery = query;

            if (string.IsNullOrWhiteSpace(filter.Filter))
            {
                return;
            }

            DateTime dateTime;
            if (!DateTime.TryParse(filter.Filter, out dateTime))
            {
                return;
            }

            // get all DateTime fields
            List<PropertyInfo> propInfos = typeof(TEntity).GetProperties()
                .Where(x => x.PropertyType == typeof(DateTime)
                            || x.PropertyType == typeof(DateTime?))
                .ToList();

            if (propInfos.Count == 0)
            {
                return;
            }

            Expression<Func<TEntity, bool>> filterExpression = x => false;

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            ConstantExpression constant = Expression.Constant(dateTime);

            foreach (PropertyInfo info in propInfos)
            {
                MemberExpression property = Expression.Property(parameter, info);
                BinaryExpression body = Expression.Equal(property, constant);

                filterExpression = Expression.Lambda<Func<TEntity, bool>>(
                    Expression.Or(filterExpression.Body, body),
                    parameter);
            }

            filteredQuery = filteredQuery
                .Where(filterExpression);
        }

        protected void TryAddStringFilter(IQueryable<TEntity> query, DataSourceFilter filter, out IQueryable<TEntity> filteredQuery)
        {
            filteredQuery = query;

            if (string.IsNullOrWhiteSpace(filter.Filter))
            {
                return;
            }

            if (!IsString(filter))
            {
                return;
            }
            

            List<Expression<Func<TEntity, string>>> filterProperties = FilterProperties()
                .ToList();

            if (filterProperties.Count == 0)
            {
                return;
            }

            Expression<Func<TEntity, bool>> filterExpression = x => false;

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            ConstantExpression constant = Expression.Constant(filter.Filter);

            MethodInfo containsMethod = GetContainsMethodInfo();

            foreach (var property in filterProperties)
            {
                var invoke = Expression.Invoke(property, parameter);

                MethodCallExpression expression = Expression.Call(
                    invoke,
                    containsMethod,
                    constant
                    );

                var lambda = Expression.Lambda<Func<TEntity, bool>>(expression, parameter);

                filterExpression = Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Or(filterExpression.Body, lambda.Body),
                        parameter);
            }

            filteredQuery = filteredQuery
                .Where(filterExpression);
        }

        private MethodInfo GetContainsMethodInfo()
        {
            Type type = typeof(string);
            MethodInfo method = type.GetMethod("Contains", new[] { typeof(string) });

            return method;
        }

        private bool IsString(DataSourceFilter filter)
        {
            if (int.TryParse(filter.Filter, out _))
            {
                return false;
            }

            if (DateTime.TryParse(filter.Filter, out _))
            {
                return false;
            }

            return true;
        }
    }
}
