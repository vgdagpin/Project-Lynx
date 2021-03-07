using Lynx.Commands.UserLoginCmds;
using Lynx.Commands.UserSessionCmds;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Common.Interfaces;
using Lynx.MobileApp.Views;
using Microsoft.EntityFrameworkCore;
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
        #region Username
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        #endregion

        #region Password
        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        #endregion

        #region ErrorMessage
        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            set => SetProperty(ref errorMessage, value);
        }
        #endregion



        public ICommand LoginCommand => new Command(OnLoginClicked);

        private async void OnLoginClicked(object obj)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var cmd = new ValidateUserLoginCmd(Username, Password);

            var loginResult = await TasqR.RunAsync(cmd);

            IsBusy = false;

            if (loginResult.IsSuccess)
            {
                Xamarin.Forms.Application.Current.MainPage = new AppShell();
            }
            else
            {
                ErrorMessage = loginResult.Error?.Message;
            }
        }
    }
}