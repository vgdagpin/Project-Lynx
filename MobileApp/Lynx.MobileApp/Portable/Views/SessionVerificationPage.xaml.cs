using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionVerificationPage : ContentPage
    {
        private SessionVerificationViewModel ViewModel = null;

        public SessionVerificationPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = App.ServiceProvider.GetService<SessionVerificationViewModel>();
        }

        private bool isStarted = false;
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);

            if (!isStarted)
            {
                isStarted = true;

                ViewModel.ValidateSession().Wait();
            }
        }
    }
}