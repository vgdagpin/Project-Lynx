using System;
using System.IO;
using Lynx.Infrastructure.Common;
using Lynx.Infrastructure.Persistence;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lynx.Infrastructure
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            return services;
        }

        public static IServiceCollection AddInfrastructureUseSqlServer(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            services.AddDbContext<LynxDbContext>((svc, options) =>
            {
                options.UseSqlServer
                (
                    connectionString: configuration.GetConnectionString($"{nameof(LynxDbContext)}_MSSQLConStr"),
                    sqlServerOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly("Lynx.DbMigration.SqlServer");
                        opt.MigrationsHistoryTable("MigrationHistory", "admin");
                        //opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }
                );

                options.UseLoggerFactory(loggerFactory);
            });

            services.AddScoped<ILynxDbContext>(provider => provider.GetService<LynxDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<LynxDbContext>());

            services.AddCommonServices(configuration);

            return services;
        }

        public static IServiceCollection AddInfrastructureUseSQLite(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory, string overridenFile = null)
        {
            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (configuration["AppDataDirectory"] != null)
            {
                appDataDirectory = configuration["AppDataDirectory"];
            }

            if (!Directory.Exists(appDataDirectory))
            {
                Directory.CreateDirectory(appDataDirectory);
            }

            string fileName = Path.Combine(appDataDirectory, $"LynxDb_SQLite.db3");
            string fileNameFromSettings = configuration.GetConnectionString($"{nameof(LynxDbContext)}_SQLiteFile");

            if (!string.IsNullOrEmpty(fileNameFromSettings))
            {
                fileName = fileNameFromSettings;
            }

            if (!string.IsNullOrWhiteSpace(overridenFile))
            {
                fileName = overridenFile;
            }

            services.AddDbContext<LynxDbContext>((svc, options) =>
            {
                options.UseSqlite
                (
                    connectionString: $"Filename={fileName}",
                    sqliteOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly("Lynx.DbMigration.SQLite");
                    }
                );

                options.UseLoggerFactory(loggerFactory);
            });

            services.AddScoped<ILynxDbContext>(provider => provider.GetService<LynxDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<LynxDbContext>());

            services.AddCommonServices(configuration);

            return services;
        }
    }
}
