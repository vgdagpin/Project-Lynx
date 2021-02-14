using System;
using System.IO;
using Lynx.Application.Common.Interfaces;
using Lynx.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lynx.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUseSqlServer(this IServiceCollection services, IConfiguration configuration)
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
                    }
                );
            });

            services.AddScoped<ILynxDbContext>(provider => provider.GetService<LynxDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<LynxDbContext>());

            return services;
        }

        public static IServiceCollection AddInfrastructureUseSQLite(this IServiceCollection services, IConfiguration configuration)
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

            string conString = $"Filename={Path.Combine(appDataDirectory, $"LynxDb_SQLite.db3")}";
            string conStringFromSettings = configuration.GetConnectionString($"{nameof(LynxDbContext)}_SQLiteConStr");

            if (!string.IsNullOrEmpty(conStringFromSettings))
            {
                conString = conStringFromSettings;
            }

            services.AddDbContext<LynxDbContext>((svc, options) =>
            {
                options.UseSqlite
                (
                    connectionString: conString,
                    sqliteOptionsAction: opt =>
                    {
                        opt.MigrationsAssembly("Lynx.DbMigration.SQLite");
                    }
                );
            });

            services.AddScoped<ILynxDbContext>(provider => provider.GetService<LynxDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<LynxDbContext>());

            return services;
        }
    }
}
