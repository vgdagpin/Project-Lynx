using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xamarin.Forms;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

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

            MainPage = new StartShell();
        }

        protected override void OnStart()
        {
            AppCenter.Start("835c5c97-a574-4ee4-86b1-eb9ba239e835", typeof(Push));
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
