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
    public partial class LogoutPage : ContentPage
    {
        private ITasqR p_TasqR;
        protected ITasqR TasqR
        {
            get
            {
                if (p_TasqR == null)
                {
                    p_TasqR = App.ServiceProvider.GetService<ITasqR>();
                }

                return p_TasqR;
            }
        }

        public LogoutPage()
        {
            InitializeComponent();

            TasqR.RunAsync(new LogoutSessionCmd());
        }
    }
}