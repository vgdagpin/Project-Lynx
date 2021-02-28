using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Views;
using Lynx.Queries.UserSessionQrs;
using TasqR;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class SessionVerificationViewModel : BaseViewModel
    {
        private readonly ITasqR p_TasqR;

        public ICommand OnNavigateLoginCommand => new Command(async () => await GoToLogin());
        public ICommand OnNavigateMainPageCommand => new Command(() => GoToMainPage());

        public SessionVerificationViewModel(ITasqR tasqR)
        {
            p_TasqR = tasqR;
        }

        public async Task ValidateSession()
        {
            //var activeSession = p_TasqR.Run(new GetActiveUserSessionQr());

            //if (activeSession != null)
            //{
            //    Xamarin.Forms.Application.Current.Properties[TokenConstant.AppTokenKey] = activeSession;

            //    GoToMainPage();
            //}
            //else
            //{
            //    await GoToLogin();
            //}
        }

        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private void GoToMainPage()
        {
            Xamarin.Forms.Application.Current.MainPage = new AppShell();
        }
    }
}
