using Microsoft.Extensions.DependencyInjection;

namespace TGPR.Database.Encryption.Operational
{
    public static class DependencyExtensions
    {
        public static void AddEncryption(this IServiceCollection services)
        {
            services.AddSingleton<IHashProvider, HashProvider>();
        }
    }
}
