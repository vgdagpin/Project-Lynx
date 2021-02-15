using Lynx.MobileApp.Services;
using Lynx.MobileApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TasqR;
using TasqR.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp
{
    public partial class App : Xamarin.Forms.Application
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            ServiceProvider = new ServiceCollection()
                .AddMobileAppPortable()
                .BuildServiceProvider();

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            
            MainPage = new StartShell();
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
