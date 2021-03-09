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

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory = null)
        {
            services.AddScoped<ILynxDbContext>(provider => provider.GetService<LynxDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetService<LynxDbContext>());
            services.AddCommonServices(configuration);

            return services;
        }
    }
}
