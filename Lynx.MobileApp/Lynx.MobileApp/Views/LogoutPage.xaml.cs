using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Commands.UserSessionCmds;
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

            LogoutStatus = "Logging out..";

            TasqR.RunAsync(new LogoutSessionCmd())
                .ContinueWith(t =>
                {
                    LogoutStatus = "Logged out";
                });
        }
    }
}