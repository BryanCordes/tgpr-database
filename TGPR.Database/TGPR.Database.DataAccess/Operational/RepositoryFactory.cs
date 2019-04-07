using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TGPR.Database.DataAccess.Context;

namespace TGPR.Database.DataAccess.Operational
{
    public interface IRepositoryFactory
    {
        TRepository Create<TRepository>()
            where TRepository : class;
    }

    internal sealed class RepositoryFactory : IRepositoryFactory
    {
        private static Lazy<ServiceProvider> _serviceProvider;

        public static void Register(Action<DbContextOptionsBuilder> optionBuilder)
        {
            IServiceCollection collection = new ServiceCollection();

            collection.AddDbContext<TgprContext>(optionBuilder, ServiceLifetime.Transient);

            var repositories = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.StartsWith("TGPR.Database.DataAccess"))
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract
                            && !string.IsNullOrWhiteSpace(x.Namespace)
                            && x.Namespace.StartsWith("TGPR.Database.DataAccess.Repositories"))
                .Select(x => new
                {
                    Implementation = x,
                    Service = x.GetInterface($"I{x.Name}")
                })
                .Where(x => x.Service != null);

            foreach (var repo in repositories)
            {
                collection.AddTransient(repo.Service, repo.Implementation);
            }

            ServiceProvider provider = collection.BuildServiceProvider();

            _serviceProvider = new Lazy<ServiceProvider>(provider);
        }

        public TRepository Create<TRepository>()
            where TRepository : class
        {
            return _serviceProvider.Value.GetService<TRepository>();
        }
    }
}
