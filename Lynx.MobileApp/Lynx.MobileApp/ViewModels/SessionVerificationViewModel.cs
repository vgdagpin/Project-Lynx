using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppX = Xamarin.Forms.Application;

namespace Lynx.MobileApp.ViewModels
{
    public class SessionVerificationViewModel : BaseViewModel
    {
        public ICommand OnNavigateLoginCommand => new Command(async () => await GoToLogin());
        public ICommand OnNavigateMainPageCommand => new Command(() => GoToMainPage());

        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private void GoToMainPage()
        {
            AppX.Current.MainPage = new AppShell();
        }
    }
}
