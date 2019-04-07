using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TGPR.Database.Authentication.DataAccess.Operational
{
    public static class AuthenticationDatabaseExtensions
    {
        public static void TryCreateAuthenticationConfigurationDatabase(this IApplicationBuilder app)
        {
            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (IServiceScope serviceScope = factory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.EnsureCreated();

                context.Database.Migrate();
            }
        }

        public static void TryCreateAuthenticationPersistanceDatabase(this IApplicationBuilder app)
        {
            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (IServiceScope serviceScope = factory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();

                context.Database.EnsureCreated();

                context.Database.Migrate();
            }
        }
    }
}
