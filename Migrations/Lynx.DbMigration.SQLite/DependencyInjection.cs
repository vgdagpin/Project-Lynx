using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lynx.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lynx.DbMigration.SQLite
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUseSqlite(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory = null, string overridenFile = null)
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

                options.EnableSensitiveDataLogging();

                if (loggerFactory != null)
                {
                    options.UseLoggerFactory(loggerFactory);
                }
            });

            return services;
        }
    }
}
