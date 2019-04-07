using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TGPR.Database.Common.Attributes;
using TGPR.Database.Components.Infrastructure;
using TGPR.Database.Components.Mapping.Profiles;
using TGPR.Database.Components.Mapping.Profiles.Applications;
using TGPR.Database.Components.Mapping.Profiles.Users;

namespace TGPR.Database.Components.Operational
{
    public static class DependencyExtensions
    {
        public static void AddComponents(this IServiceCollection services)
        {
            string[] blacklistNamespaces =
            {
                "TGPR.Database.Components.Operational",
                "TGPR.Database.Components.Mappers",
                "TGPR.Database.Components.Applications.QuestionTypeOptions"
            };

            var repositories = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.StartsWith("TGPR.Database.Components"))
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract
                            && !string.IsNullOrWhiteSpace(x.Namespace)
                            && blacklistNamespaces.All(n => n != x.Namespace))
                .Select(x => new
                {
                    Implementation = x,
                    Service = x.GetInterface($"I{x.Name}")
                })
                .Where(x => x.Service != null)
                .ToList();

            foreach (var repo in repositories)
            {
                services.AddTransient(repo.Service, repo.Implementation);
            }

            // QuestionTypeOptions
            var questionTypeOptions = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.StartsWith("TGPR.Database.Components"))
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetInterfaces()
                    .Any(i => i == typeof(IQuestionTypeOptions)));
            foreach (var questionTypeOption in questionTypeOptions)
            {
                var descriptor = new ServiceDescriptor(typeof(IQuestionTypeOptions), questionTypeOption, ServiceLifetime.Transient);

                services.Add(descriptor);
            }

            services.AddMappers();
        }

        private static void AddMappers(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(config =>
            {
                Profile[] profiles =
                {
                    new UserProfile(),
                    new RoleProfile(), 
                    new ApplicationTemplateProfile(),
                    new ApplicationCategoryProfile(),
                    new ApplicationQuestionProfile(), 
                    new ApplicationOptionProfile(), 
                };

                foreach (var profile in profiles)
                {
                    config.AddProfile(profile);
                }

                config.AddGlobalIgnore("ObjectState");

                //config.ForAllPropertyMaps(map =>
                //    map.SourceMember != null
                //    && map.SourceMember.GetCustomAttributes(true)
                //        .OfType<MapIgnoreAttribute>()
                //        .Any(),
                //    (map, c) =>
                //    {
                //        c.Ignore();
                //    }
                //);
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
