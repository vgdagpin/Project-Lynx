using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Commands.UserSessionCmds;
using Lynx.MobileApp.Handlers.Commands.UserSessionCmds;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogoutPage : LynxContentPage
    {
        #region LogoutStatus
        private string logoutStatus;
        public string LogoutStatus
        {
            get => logoutStatus;
            set => SetProperty(ref logoutStatus, value);
        }
        #endregion

        public LogoutPage()
        {
            InitializeComponent();

            BindingContext = this;

            LogoutStatus = "Logging out..";

            Logout();
        }

        private void Logout()
        {
            Task.Run(async () =>
            {
                try
                {                    
                    await TasqR
                    .UsingAsHandler<LogoutSessionCmdHandler_API>()
                    .RunAsync(new LogoutSessionCmd());

                    LogoutStatus = "Logged out";

                    //Xamarin.Forms.Application.Current.MainPage = new StartShell();
                    //await Shell.Current.GoToAsync("//LoginPage");
                }
                catch (Exception ex)
                {
                    LogoutStatus = ex.InnermostException().Message;
                }
            });
        }
    }
}