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
            Task.Run(() =>
            {
                try
                {
                    IsBusy = true;

                    TasqR.RunAsync(new GetUserBillsQr(AppUser.UserID))
                        .ContinueWith(bills =>
                        {
                            Bills.Clear();

                            foreach (var item in bills.Result)
                            {
                                Bills.Add(item);
                            }

                            IsBusy = false;
                        });
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogError(ex);
                }
            });
        }
    }
}