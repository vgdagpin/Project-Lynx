using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionVerificationPage : ContentPage
    {
        public SessionVerificationPage()
        {
            InitializeComponent();
        }

        public async void Init()
        {
            Thread.Sleep(5000);
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}