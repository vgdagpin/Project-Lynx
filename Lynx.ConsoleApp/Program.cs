using Lynx.Application;
using Lynx.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TasqR;

namespace Lynx.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainService = CreateHostBuilder(args).Build().Services;
        }



        static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructureUseSqlServer(configuration);
            services.AddApplication();

            services.AddTasqR(Assembly.GetExecutingAssembly());

            services.AddMemoryCache();
        }


        #region HostBuilder
        static AppServiceBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var builder = new AppServiceBuilder();

            ConfigureServices(builder.Services, config);

            return builder;
        }

        class AppServiceBuilder
        {
            public ServiceCollection Services { get; } = new ServiceCollection();
            public AppServiceProvider Build() => new AppServiceProvider(Services.BuildServiceProvider());
        }

        class AppServiceProvider
        {
            public AppServiceProvider(IServiceProvider services) { Services = services; }
            public IServiceProvider Services { get; }
        }
        #endregion
    }
}
