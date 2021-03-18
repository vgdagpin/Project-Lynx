using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Handlers.Queries.BillsQrs;
using Lynx.MobileApp.Handlers.Queries.UserBillQrs;
using Lynx.Queries.UserBillQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region TotalDue
        private decimal totalDue;
        public decimal TotalDue
        {
            get => totalDue;
            set => SetProperty(ref totalDue, value);
        }
        #endregion





        public ObservableCollection<UserBillSummaryVM> Bills { get; protected set; } = new ObservableCollection<UserBillSummaryVM>();

        public Command<UserBillSummaryVM> ItemTapped { get; } = new Command<UserBillSummaryVM>(async bill =>
        {
            await Shell.Current.GoToUserBillDetailPage(bill.ID);
        });

        public ICommand LoadData { get; }

        public HomePageViewModel()
        {
            LoadData = new Command(async () => await LoadDataCommand());
        }

        private async Task LoadDataCommand()
        {
            IsBusy = true;
            IsLoaded = false;

            try
            {
                var bills = await TasqR
                    .UsingAsHandler<GetUserBillsQrHandler_API>()
                    .RunAsync(new GetUserBillsQr(AppUser.UserID, 30));

                Bills.Clear();

                foreach (var item in bills)
                {
                    Bills.Add(item);
                }

                TotalDue = bills.Sum(a => a.Amount);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            IsLoaded = true;
            IsBusy = false;
        }
    }
}