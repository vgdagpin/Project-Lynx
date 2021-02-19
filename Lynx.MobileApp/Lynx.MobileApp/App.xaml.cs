using Lynx.MobileApp.Common;
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

            var handlerResolver = new TasqHandlerResolver();
            handlerResolver.RegisterFromAssembly(Assembly.GetExecutingAssembly());
            var tasqR = new TasqRObject(handlerResolver);

            DependencyService.RegisterSingleton<ITasqR>(tasqR);

            DependencyService.Register<AppDateTime>();
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
