using System;
using System.IO;
using System.Threading.Tasks;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}