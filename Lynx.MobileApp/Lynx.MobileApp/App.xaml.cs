using System;
using Microsoft.Extensions.DependencyInjection;

namespace Lynx.MobileApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceProvider = new ServiceCollection()
                .AddMobileAppPortable()
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
