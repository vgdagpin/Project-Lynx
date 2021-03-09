using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Infrastructure.Common;
using Lynx.Infrastructure.Persistence;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lynx.DbMigration.SqlServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureUseSqlServer(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory = null)
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

                if (loggerFactory != null)
                {
                    options.UseLoggerFactory(loggerFactory);
                }
            });

            

            return services;
        }
    }
}
