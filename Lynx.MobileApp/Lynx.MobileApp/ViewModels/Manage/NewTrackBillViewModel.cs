using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Commands.TrackBillCmds;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Handlers.Queries.BillsQrs;
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

                BillsProviderLoadIndicator = "Select bill provider";
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
            set 
            {
                SetProperty(ref selectedBillProvider, value);

                if (selectedBillProvider != null)
                {
                    switch (selectedBillProvider.ProviderTypeID)
                    {
                        case ProviderTypeConstants.Scheduled:
                            LoadScheduledProviderOptions();
                            break;
                        case ProviderTypeConstants.Email:
                            LoadEmailProviderOptions();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    ClearProviderOptions();
                }
                
            }
        }
        #endregion


        #region BillsLoaded
        private bool billsLoaded;
        public bool BillsLoaded
        {
            get => billsLoaded;
            set => SetProperty(ref billsLoaded, value);
        }
        #endregion

        #region BillsProviderLoadIndicator
        private string billsProviderLoadIndicator;
        public string BillsProviderLoadIndicator
        {
            get => billsProviderLoadIndicator;
            set => SetProperty(ref billsProviderLoadIndicator, value);
        }
        #endregion


        #region IsEmailProviderOption
        private bool isEmailProviderOption;
        public bool IsEmailProviderOption
        {
            get => isEmailProviderOption;
            set => SetProperty(ref isEmailProviderOption, value);
        }
        #endregion

        #region IsScheduledProviderOption
        private bool isScheduledProviderOption;
        public bool IsScheduledProviderOption
        {
            get => isScheduledProviderOption;
            set => SetProperty(ref isScheduledProviderOption, value);
        }
        #endregion




        public ObservableCollection<BillSummaryVM> Bills { get; protected set; } = new ObservableCollection<BillSummaryVM>();
        public ObservableCollection<BillProviderVM> BillProviders { get; protected set; } = new ObservableCollection<BillProviderVM>();

        public ICommand LoadBillsCommand { get; }

        public ICommand SaveChanges { get; }

        public NewTrackBillViewModel()
        {
            SaveChanges = new Command(async () => await SaveChangesAsync());
            LoadBillsCommand = new Command(async () => await LoadBills());
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

        private Task LoadBills()
        {
            return Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;
                    BillsLoaded = false;
                    BillsLoadIndicator = "Loading Bills..";
                    BillsProviderLoadIndicator = "Loading Bills..";

                    var bills = await TasqR
                        .UsingAsHandler<GetBillsQrHandler_API>()
                        .RunAsync(new GetBillsQr());


                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Bills.Clear();

                        foreach (var item in bills)
                        {
                            Bills.Add(item);
                        }

                        BillsLoadIndicator = "Select Bills to Track";
                        BillsProviderLoadIndicator = "No bill selected";
                    });
                }
                catch (Exception ex)
                {
                    BillsLoadIndicator = "Bills Loading Error";
                    BillsProviderLoadIndicator = "Bills Loading Error";
                    LogError(ex);
                }

                IsBusy = false;
                BillsLoaded = true;
            });
        }

        private void ClearProviderOptions()
        {
            IsScheduledProviderOption = false;
            IsEmailProviderOption = false;
        }

        private void LoadScheduledProviderOptions()
        {
            IsScheduledProviderOption = true;
            IsEmailProviderOption = false;
        }

        private void LoadEmailProviderOptions()
        {
            IsScheduledProviderOption = false;
            IsEmailProviderOption = true;
        }
    }
}