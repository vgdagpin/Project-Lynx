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
        private readonly ITasqR p_TasqR;
        private readonly DbContext p_BaseDbContext;
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


        public ICommand LoginCommand => new Command(OnLoginClicked);

        public LoginViewModel(ITasqR tasqR, DbContext baseDbContext)
        {
            p_TasqR = tasqR;
            p_BaseDbContext = baseDbContext;
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                var cmd = new ValidateUserLoginCmd(p_Username, p_Password);

                var loginResult = await p_TasqR.RunAsync(cmd);

                if (loginResult.IsSuccess)
                {
                    var newSessionCmd = new CreateSessionCmd(p_Username);
                    var sessionResult = await p_TasqR.RunAsync(newSessionCmd);

                    await p_BaseDbContext.SaveChangesAsync();

                    Application.Current.Properties[TokenConstant.AppTokenKey] = sessionResult;
                    Application.Current.MainPage = new AppShell();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}