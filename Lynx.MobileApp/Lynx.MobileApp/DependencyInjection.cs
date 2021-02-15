using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Lynx.Infrastructure;
using Lynx.Infrastructure.Common;
using Lynx.Interfaces;
using Lynx.MobileApp.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TasqR;
using Xamarin.Essentials;

namespace Lynx.MobileApp
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMobileAppPortable(this IServiceCollection services)
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

                    configuration["AppDataDirectory"] = FileSystem.AppDataDirectory;
                }
            }

            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddInfrastructureUseSQLite(configuration);
            services.AddTasqR(Assembly.GetExecutingAssembly());

            services.AddTransient<SessionVerificationViewModel>();
            services.AddTransient<LoginViewModel>();


            return services;
        }
    }
}
