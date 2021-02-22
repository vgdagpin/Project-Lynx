using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Queries.BillsQrs;
using Lynx.Queries.TrackBillsQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels.Manage
{
    public class NewTrackBillViewModel : BaseViewModel
    {
        public ObservableCollection<BillSummaryVM> Bills { get; protected set; } = new ObservableCollection<BillSummaryVM>();
        public ObservableCollection<BillProviderVM> BillProviders { get; protected set; } = new ObservableCollection<BillProviderVM>();

        private bool billProviderLoaded;
        public bool BillProviderLoaded
        {
            get { return billProviderLoaded; }
            set { if (billProviderLoaded != value) SetProperty(ref billProviderLoaded, value); }
        }


        private BillSummaryVM selectedBill;
        public BillSummaryVM SelectedBill
        {
            get { return selectedBill; }
            set
            {
                SetProperty(ref selectedBill, value);
                BillProviderLoaded = true;

                BillProviders.Clear();

                foreach (var provider in selectedBill.Providers)
                {
                    BillProviders.Add(provider);
                }
            }
        }

        private BillProviderVM selectedBillProvider;
        public BillProviderVM SelectedBillProvider
        {
            get { return selectedBillProvider; }
            set { SetProperty(ref selectedBillProvider, value); }
        }


        private string shortDesc;
        public string ShortDesc
        {
            get { return shortDesc; }
            set { SetProperty(ref shortDesc, value); }
        }

        private string billsLoadIndicator;

        public string BillsLoadIndicator
        {
            get { return billsLoadIndicator; }
            set { SetProperty(ref billsLoadIndicator, value); }
        }

        public NewTrackBillViewModel()
        {
            LoadBills();
        }

        private void LoadBills()
        {
            Task.Run(() =>
            {
                try
                {
                    IsBusy = true;
                    BillsLoadIndicator = "Loading Bills..";

                    TasqR.RunAsync(new GetBillsQr())
                        .ContinueWith(bills =>
                        {
                            Bills.Clear();

                            foreach (var item in bills.Result)
                            {
                                Bills.Add(item);
                            }

                            IsBusy = false;
                            BillsLoadIndicator = "Select Bills to Track";
                        });
                }
                catch (Exception ex)
                {
                    BillsLoadIndicator = "Bills Loading Error";
                    ExceptionHandler.LogError(ex);
                }

                IsBusy = false;
            });
        }
    }
}
