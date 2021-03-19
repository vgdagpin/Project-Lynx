using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class AboutViewModel : LynxViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));

        }

        public ICommand OpenWebCommand { get; }
    }
}