using System;
using Lynx.MobileApp.Views;
using Lynx.MobileApp.Views.Home;
using Lynx.MobileApp.Views.Manage;
using Xamarin.Forms;

namespace Lynx.MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();


            Routing.RegisterRoute(nameof(UserBillDetailPage), typeof(UserBillDetailPage));
            Routing.RegisterRoute(nameof(PayByMyselfPage), typeof(PayByMyselfPage));
            Routing.RegisterRoute(nameof(PayWithLynxPage), typeof(PayWithLynxPage));

            Routing.RegisterRoute(nameof(NewTrackBillPage), typeof(NewTrackBillPage));
            Routing.RegisterRoute(nameof(TrackBillPage), typeof(TrackBillPage));
        }
    }
}