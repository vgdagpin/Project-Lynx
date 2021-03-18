using System;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;
using Lynx.MobileApp.Views;
using Lynx.MobileApp.Views.Home;
using Xamarin.Forms;

namespace Lynx.MobileApp
{
    public static class ShellExtensions
    {
        public static Task GoToUserBillDetailPage(this Shell shell, Guid userBillID)
        {
            return shell.GoToAsync($"{nameof(UserBillDetailPage)}?{nameof(UserBillDetailViewModel.UserBillID)}={userBillID}");
        }

        public static Task GoToPayWithLynxPage(this Shell shell, Guid userBillID)
        {
            return shell.GoToAsync($"{nameof(PayWithLynxPage)}?{nameof(UserBillDetailViewModel.UserBillID)}={userBillID}");
        }

        public static Task GoToPayByMyselfPage(this Shell shell, Guid userBillID)
        {
            return shell.GoToAsync($"{nameof(PayByMyselfPage)}?{nameof(UserBillDetailViewModel.UserBillID)}={userBillID}");
        }

        public static Task GoToManageBillPage(this Shell shell)
        {
            return shell.GoToAsync(nameof(ManageBillPage));
        }

        public static Task GoToTrackBillDetailPage(this Shell shell, Guid trackBillID)
        {
            return shell.GoToAsync($"{nameof(TrackBillPage)}?{nameof(TrackBillViewModel.TrackBillID)}={trackBillID}");
        }

        public static Task GoToPage<T>(this Shell shell) where T : ContentPage
        {
            return shell.GoToAsync($"{typeof(T).Name}");
        }
    }
}