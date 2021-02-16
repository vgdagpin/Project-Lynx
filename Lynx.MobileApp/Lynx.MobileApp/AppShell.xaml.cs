using Lynx.MobileApp.ViewModels;
using Lynx.MobileApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Lynx.MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            
            Routing.RegisterRoute(nameof(BillDetail), typeof(BillDetail));
            Routing.RegisterRoute(nameof(TrackBillPage), typeof(TrackBillPage));
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
