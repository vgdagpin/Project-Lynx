using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Infrastructure.Persistence;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace LynxApplicationTests.Base
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureUseInMemory(this IServiceCollection services, string databaseName)
        {
            services.AddDbContext<LynxDbContext>(opt =>
            {
                opt.UseInMemoryDatabase(databaseName: databaseName);
                opt.ConfigureWarnings(a => a.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            return services;
        }
    }
}
