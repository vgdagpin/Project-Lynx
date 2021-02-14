using Lynx.MobileApp.Services;
using Lynx.MobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
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
