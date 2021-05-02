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
    public class LoginViewModel : LynxViewModel
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

        #region LoginButtonText
        private string loginButtonText;
        public string LoginButtonText
        {
            get => loginButtonText;
            set => SetProperty(ref loginButtonText, value);
        }
        #endregion

        public ICommand LoginCommand { get; }


        public LoginViewModel()
        {
            LoginButtonText = "Login";

            LoginCommand = new Command(() => OnLoginClicked());
        }



        private async void OnLoginClicked()
        {
            if (string.IsNullOrWhiteSpace(Username)
                || string.IsNullOrWhiteSpace(Password))
            {
                return;
            }

            EnableFlag = false;
            LoginButtonText = "Logging in..";
            ErrorMessage = "";

            var cmd = new ValidateUserLoginCmd(Username, Password);

            var loginResult = await TasqR.RunAsync(cmd);

            if (loginResult.IsSuccess)
            {
                Xamarin.Forms.Application.Current.MainPage = new AppShell();
            }
            else
            {
                // ErrorMessage = loginResult.Error?.Message;
                ErrorMessage = "Invalid username or password";

                LoginButtonText = "Login";
                EnableFlag = true;
            }
        }
    }
}