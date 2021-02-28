using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Views;
using Lynx.MobileApp.Views.Manage;
using Lynx.Queries.TrackBillsQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class ManageBillViewModel : BaseViewModel
    {
        public ObservableCollection<TrackBillSummaryVM> TrackedBills { get; protected set; } = new ObservableCollection<TrackBillSummaryVM>();

        public Command<TrackBillSummaryVM> ItemTapped { get; } = new Command<TrackBillSummaryVM>(trackBill => Shell.Current.GoToTrackBillDetailPage(trackBill.ID));
        public ICommand LoadData { get; }
        public ICommand TrackNewBill { get; } = new Command(() => Shell.Current.GoToPage<NewTrackBillPage>());

        public ManageBillViewModel()
        {
            LoadData = new Command(async () => await LoadDataCommand());
        }

        private async Task LoadDataCommand()
        {
            try
            {
                var bills = await TasqR.RunAsync(new GetUserTrackedBillsQr(AppUser.UserID));

                TrackedBills.Clear();

                foreach (var item in bills)
                {
                    TrackedBills.Add(item);
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {

                ExceptionHandler.LogError(ex);
            }
        }
    }
}