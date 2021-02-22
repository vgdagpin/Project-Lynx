using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

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
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
