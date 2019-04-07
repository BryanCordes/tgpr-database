using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TGPR.Database.DataAccess.Context;

namespace TGPR.Database.DataAccess.Operational
{
    public static class DatabaseExtensions
    {
        public static void TryCreateDatabase(this IApplicationBuilder app)
        {
            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (IServiceScope serviceScope = factory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TgprContext>();

                context.Database.EnsureCreated();
            }

            app.TryAddConstraints();
            app.TryAddData();
            app.TryMigrate();
        }

        private static void TryAddData(this IApplicationBuilder app)
        {
            string filePath = GetSqlDataFilePath();
            string contents = File.ReadAllText(filePath);

            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (IServiceScope serviceScope = factory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TgprContext>();

                int lines = context.Version
                    .FromSql(contents)
                    .Count();
            }
        }

        private static void TryAddConstraints(this IApplicationBuilder app)
        {
            string filePath = GetSqlConstraintsPath();
            string contents = File.ReadAllText(filePath);

            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (IServiceScope serviceScope = factory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TgprContext>();

                int lines = context.Version
                    .FromSql(contents)
                    .Count();
            }
        }

        private static void TryMigrate(this IApplicationBuilder app)
        {
            IEnumerable<string> filePaths = GetMigrationPaths();

            var factory = app.ApplicationServices.GetService<IServiceScopeFactory>();

            using (IServiceScope serviceScope = factory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TgprContext>();

                foreach (string filePath in filePaths)
                {
                    string contents = File.ReadAllText(filePath);

                    int lines = context.Version
                        .FromSql(contents)
                        .Count();
                }
            }
        }

        private static string GetSqlDataFilePath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string directory = Path.Combine(baseDirectory, "_Sql");

            string fileName = "TGPR_Application_data.sql";

            string filePath = Path.Combine(directory, fileName);

            return filePath;
        }

        private static string GetSqlConstraintsPath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string directory = Path.Combine(baseDirectory, "_Sql");

            string fileName = "TGPR_Application_constraints.sql";

            string filePath = Path.Combine(directory, fileName);

            return filePath;
        }

        private static IEnumerable<string> GetMigrationPaths()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string directory = Path.Combine(baseDirectory, "_Sql");

            string migrationDirectory = Path.Combine(directory, "Migrations");

            if (!Directory.Exists(migrationDirectory))
            {
                // There are no migration files
                return Enumerable.Empty<string>();
            }

            var versionComparer = new VersionComparer();

            List<string> files = Directory.GetFiles(migrationDirectory)
                .OrderBy(GetVersion, versionComparer)
                .ToList();

            return files;
        }

        private static int[] GetVersion(string filePath)
        {
            string fileName = Path.GetFileName(filePath);

            string version = fileName.Replace("v", string.Empty).Replace(".sql", string.Empty);

            int[] versionParts = version.Split('.')
                .Select(int.Parse)
                .ToArray();

            return versionParts;
        }
    }

    internal class VersionComparer : IComparer<int[]>
    {
        // ReSharper disable PossibleNullReferenceException
        public int Compare(int[] x, int[] y)
        {
            int len = x.Length > y.Length
                ? y.Length
                : x.Length;
            for (int i = 0; i < len; i++)
            {
                if (x[i] == y[i])
                {
                    continue;
                }

                return x[i] < y[i]
                    ? -1
                    : 1;
            }

            return x.Length < y.Length
                ? -1
                : 1;
        }
        // ReSharper restore PossibleNullReferenceException
    }
}