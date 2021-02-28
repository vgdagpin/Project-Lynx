using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionVerificationPage : ContentPage
    {
        private SessionVerificationViewModel ViewModel = null;

        public SessionVerificationPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = App.ServiceProvider.GetService<SessionVerificationViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(() =>
            {
                return ViewModel.ValidateSession()
                    .ContinueWith(r =>
                    {
                        //Xamarin.Forms.Application.Current.MainPage = new AppShell();
                    });
            });
        }
    }
}