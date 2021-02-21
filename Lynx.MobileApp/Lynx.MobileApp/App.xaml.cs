using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lynx.MobileApp
{
    public partial class App : Xamarin.Forms.Application
    {
        static readonly ILoggerFactory SampleLoggingFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceProvider = new ServiceCollection()
                .AddMobileAppPortable(SampleLoggingFactory)
                .BuildServiceProvider();

            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
