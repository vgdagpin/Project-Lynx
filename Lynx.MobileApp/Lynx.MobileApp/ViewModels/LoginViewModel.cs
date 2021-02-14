using Lynx.Commands.UserLoginCmds;
using Lynx.MobileApp.Common.Interfaces;
using Lynx.MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TasqR;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ITasqR p_TasqR;
        private readonly INavigationService p_NavigationService;
        private string p_Username;
        public string Username
        {
            get { return p_Username; }
            set { SetProperty(ref p_Username, value); }
        }

        private string p_Password;
        public string Password
        {
            get { return p_Password; }
            set { SetProperty(ref p_Password, value); }
        }


        public ICommand LoginCommand { get; }

        public LoginViewModel(ITasqR tasqR, INavigationService navigationService)
        {
            LoginCommand = new Command(OnLoginClicked);
            p_TasqR = tasqR;
            p_NavigationService = navigationService;
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                var cmd = new ValidateUserLoginCmd(p_Username, p_Password);

                var loginResult = await p_TasqR.RunAsync(cmd);

                if (loginResult.IsSuccess)
                {
                    await Shell.Current.GoToAsync($"//{nameof(AppShell)}");
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
