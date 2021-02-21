using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Views;
using Lynx.Queries.TrackBillsQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class ManageBillViewModel : BaseViewModel
    {
        public ObservableCollection<TrackBillSummaryVM> TrackedBills { get; protected set; } = new ObservableCollection<TrackBillSummaryVM>();

        public Command<TrackBillSummaryVM> ItemTapped { get; } = new Command<TrackBillSummaryVM>(async trackBill =>
        {
            throw new NotImplementedException();
            //await Shell.Current.GoToUserBillDetailPage(trackBill.ID);
        });

        public ICommand LoadData { get; }


        public ICommand TrackNewBill { get; } = new Command(async () =>
        {
            await Shell.Current.GoToAsync($"{nameof(TrackBillPage)}");
        });

        public ManageBillViewModel()
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

                    var bills = await TasqR.RunAsync(new GetUserTrackedBillsQr(AppUser.UserID));

                    TrackedBills.Clear();

                    foreach (var item in bills)
                    {
                        TrackedBills.Add(item);
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
