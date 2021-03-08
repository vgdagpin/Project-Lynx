﻿using System;
using System.IO;
using System.Reflection;
using Lynx.Common;
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
        public static IServiceCollection AddMobileAppPortable(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            var embeddedResourceStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Lynx.MobileApp.config.json");

            IConfiguration configuration = null;

            if (embeddedResourceStream != null)
            {
                using (var streamReader = new StreamReader(embeddedResourceStream))
                {
                    configuration = new ConfigurationBuilder().AddJsonStream(streamReader.BaseStream)
                        .Build();

                    try
                    {
                        configuration["AppDataDirectory"] = FileSystem.AppDataDirectory;
                    }
                    catch (Exception ex)
                    {
                        loggerFactory.CreateLogger("Dependency Injection").LogError(ex, ex.Message);
                    }
                }
            }

            services.AddHttpClient("lynx-api", (provider, config) =>
            {
                config.BaseAddress = new Uri(configuration["Lynx-API-Hostname"]);

                config.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Lynx");
            });

            services.AddInfrastructureUseSQLite(configuration, loggerFactory, SQLiteConstants.FilePath);
            services.AddTasqR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IDateTime, AppDateTime>();
            services.AddSingleton<IGuid, AppGuid>();
            services.AddSingleton<IAppUser, AppUser>();
            services.AddSingleton<IJsonSerializer, AppJsonSerializer>();
            services.AddSingleton<IExceptionHandler, ExceptionHandler>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddTransient<SessionVerificationViewModel>();
            services.AddTransient<LoginViewModel>();


            return services;
        }
    }
}