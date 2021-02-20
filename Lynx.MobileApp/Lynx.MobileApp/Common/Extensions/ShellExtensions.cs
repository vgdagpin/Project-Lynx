using System;
using System.Threading.Tasks;
using Lynx.MobileApp.ViewModels;
using Lynx.MobileApp.Views.Home;
using Xamarin.Forms;

namespace Lynx.MobileApp
{
    public static class ShellExtensions
    {
        public static Task GoToUserBillDetailPage(this Shell shell, Guid userBillID)
        {
            return shell.GoToAsync($"{nameof(UserBillDetailPage)}?{nameof(BillDetailViewModel.BillID)}={userBillID}");
        }
    }
}