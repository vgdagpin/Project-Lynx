using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

            LoadData.Execute(null);
        }

        private void LoadDataCommand()
        {
            Task.Run(async () =>
            {

                try
                {
                    IsBusy = true;

                    var bills = await TasqR.RunAsync(new GetUserBillsQr(AppUser.UserID));

                    Bills.Clear();

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
            });
        }
    }
}