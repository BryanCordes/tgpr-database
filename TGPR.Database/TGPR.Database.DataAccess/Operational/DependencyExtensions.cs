using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TGPR.Database.DataAccess.Context;
using TGPR.Database.Encryption.Operational;

namespace TGPR.Database.DataAccess.Operational
{
    public static class DependencyExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
        {
            services.AddDbContext<TgprContext>(optionsBuilder);

            RepositoryFactory.Register(optionsBuilder);

            services.AddSingleton<IRepositoryFactory, RepositoryFactory>();

            // include encryption 
            services.AddEncryption();
        }
    }
}
