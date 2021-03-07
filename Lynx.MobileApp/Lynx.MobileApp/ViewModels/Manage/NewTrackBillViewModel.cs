using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Commands.TrackBillCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Queries.BillsQrs;
using Lynx.Queries.TrackBillsQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels.Manage
{
    public class NewTrackBillViewModel : BaseViewModel
    {
        #region BillProviderLoaded
        private bool billProviderLoaded;
        public bool BillProviderLoaded
        {
            get { return billProviderLoaded; }
            set { if (billProviderLoaded != value) SetProperty(ref billProviderLoaded, value); }
        }
        #endregion

        #region SelectedBill
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
        #endregion

        #region EmailAddress
        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set { SetProperty(ref emailAddress, value); }
        }
        #endregion

        #region AccountNumber
        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { SetProperty(ref accountNumber, value); }
        }
        #endregion

        #region ShortDesc
        private string shortDesc;
        public string ShortDesc
        {
            get { return shortDesc; }
            set { SetProperty(ref shortDesc, value); }
        }
        #endregion

        #region BillsLoadIndicator
        private string name;
        public string BillsLoadIndicator
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        #endregion

        #region SelectedBillProvider
        private BillProviderVM selectedBillProvider;
        public BillProviderVM SelectedBillProvider
        {
            get { return selectedBillProvider; }
            set { SetProperty(ref selectedBillProvider, value); }
        }
        #endregion





        public ObservableCollection<BillSummaryVM> Bills { get; protected set; } = new ObservableCollection<BillSummaryVM>();
        public ObservableCollection<BillProviderVM> BillProviders { get; protected set; } = new ObservableCollection<BillProviderVM>();



        public ICommand SaveChanges { get; }

        public NewTrackBillViewModel()
        {
            SaveChanges = new Command(async () => await SaveChangesAsync());

            LoadBills();
        }

        private Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (SelectedBill == null || SelectedBillProvider == null)
            {
                return Task.FromResult(0);
            }

            if (IsBusy)
            {
                return Task.FromResult(0);
            }

            IsBusy = true;

            TrackBillVM entry = new TrackBillVM
            {
                AccountNumber = AccountNumber,
                ShortDesc = ShortDesc,
                LongDesc = ShortDesc,
                Bill = new BillVM
                {
                    ID = SelectedBill.ID
                },
                BillProvider = new BillProviderVM
                {
                    ProviderTypeID = SelectedBillProvider.ProviderTypeID
                }
            };

            CreateTrackBillCmd cmd = new CreateTrackBillCmd(entry);

            return TasqR.RunAsync(cmd, cancellationToken)
                .ContinueWith(res =>
                {
                    IsBusy = false;

                    var createResult = res.Result;

                    return Shell.Current.Navigation.PopAsync();
                });
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
