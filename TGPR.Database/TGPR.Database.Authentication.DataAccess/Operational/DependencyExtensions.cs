using System;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TGPR.Database.Authentication.DataAccess.Operational
{
    public static class DependencyExtensions
    {
        public static void AddAuthenticationConfigurationDataAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            services.AddDbContext<ConfigurationDbContext>(optionsBuilder);
        }

        public static void AddAuthenticationPersistanceDataAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            services.AddDbContext<PersistedGrantDbContext>(optionsBuilder);
        }
    }
}
