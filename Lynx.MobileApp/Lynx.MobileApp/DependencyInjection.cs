using System;
using System.IO;
using System.Reflection;
using Lynx.Common;
using Lynx.DbMigration.SQLite;
using Lynx.Infrastructure;
using Lynx.Infrastructure.Common;
using Lynx.Interfaces;
using Lynx.MobileApp.Common;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Portable.Common;
using Lynx.MobileApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TasqR;
using Xamarin.Essentials;

namespace Lynx.MobileApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMobileAppPortable
            (
                this IServiceCollection services,
                IConfiguration configuration = null,
                Action<IServiceCollection> additionalServices = null, 
                ILoggerFactory loggerFactory = null
            )
        {
            services.AddHttpClient("lynx-api", (provider, config) =>
            {
                config.BaseAddress = new Uri(configuration["Lynx-API-Hostname"]);

                config.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Lynx");
            });

            services.AddInfrastructureUseSqlite(configuration, loggerFactory, SQLiteConstants.FilePath);

            services.AddInfrastructure(configuration, loggerFactory);

            if (additionalServices != null)
            {
                additionalServices.Invoke(services);
            }

            services.AddTasqR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IDateTime, AppDateTime>();
            services.AddSingleton<IGuid, AppGuid>();
            services.AddSingleton<IAppUser, AppUser>();
            services.AddSingleton<IJsonSerializer, AppJsonSerializer>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddLogging();

            services.AddTransient<SessionVerificationViewModel>();
            services.AddTransient<LoginViewModel>();

            services.AddSingleton<FirebaseTokenManager>();


            return services;
        }
    }
}
