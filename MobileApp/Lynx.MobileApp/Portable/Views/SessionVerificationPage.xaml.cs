using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.MobileApp.Portable.Common.Enums;
using Lynx.MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionVerificationPage : ContentPage
    {
        private readonly SessionVerificationViewModel ViewModel;

        public SessionVerificationPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new SessionVerificationViewModel();

            ViewModel.OnVerificationCompleteEvent += viewModel_OnVerificationCompleteEvent;
        }

        private void viewModel_OnVerificationCompleteEvent(SessionVerificationResult result, object sender)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                switch (result)
                {
                    case SessionVerificationResult.NeedLogin:
                        ViewModel.PreloadProgress += "\nNavigate Login";
                        await Shell.Current.GoToAsync("//LoginPage");
                        break;
                    case SessionVerificationResult.Authenticated:
                        ViewModel.PreloadProgress += "\nNavigate Main Page";
                        Xamarin.Forms.Application.Current.MainPage = new AppShell();
                        break;
                    default:
                        break;
                }
            });
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void btnMain_Clicked(object sender, EventArgs e)
        {
            Xamarin.Forms.Application.Current.MainPage = new AppShell();
        }
    }
}