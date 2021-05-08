using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Handlers.Queries.TrackBillsQrs;
using Lynx.MobileApp.Views;
using Lynx.MobileApp.Views.Manage;
using Lynx.Queries.TrackBillsQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class ManageBillViewModel : LynxViewModel
    {
        public ObservableCollection<TrackBillSummaryBO> TrackedBills { get; protected set; } = new ObservableCollection<TrackBillSummaryBO>();

        public Command<TrackBillSummaryBO> ItemTapped { get; } = new Command<TrackBillSummaryBO>(trackBill => Shell.Current.GoToTrackBillDetailPage(trackBill.ID));
        public ICommand LoadData { get; }
        public ICommand TrackNewBill { get; } = new Command(() => Shell.Current.GoToPage<NewTrackBillPage>());

        public ManageBillViewModel()
        {
            LoadData = new Command(async () => await LoadDataCommand());
        }

        private async Task LoadDataCommand()
        {
            IsBusy = true;

            try
            {
                var bills = await TasqR
                    .UsingAsHandler<GetTrackedBillsQrHandler_API>()
                    .RunAsync(new GetTrackBillsQr(AppUser.UserID));

                TrackedBills.Clear();

                foreach (var item in bills)
                {
                    TrackedBills.Add(item);
                }
            }
            catch (Exception ex)
            {

                LogError(ex);
            }

            IsBusy = false;
        }
    }
}