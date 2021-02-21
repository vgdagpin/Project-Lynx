using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.Domain.ViewModels;
using Lynx.Queries.UserBillQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public ObservableCollection<UserBillSummaryVM> Bills { get; protected set; } = new ObservableCollection<UserBillSummaryVM>();

        public Command<UserBillSummaryVM> ItemTapped { get; } = new Command<UserBillSummaryVM>(async bill =>
        {
            await Shell.Current.GoToUserBillDetailPage(bill.ID);
        });

        public ICommand LoadData { get; }

        public HomePageViewModel()
        {
            LoadData = new Command(LoadDataCommand);
        }

        async void LoadDataCommand()
        {
            if (IsBusy) return;

            Bills.Clear();

            try
            {
                IsBusy = true;

                var bills = await TasqR.RunAsync(new GetUserBillsQr(AppUser.UserID));

                foreach (var item in bills)
                {
                    Bills.Add(item);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }

            IsBusy = false;
        }
    }
}
