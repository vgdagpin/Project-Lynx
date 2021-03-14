using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.MobileApp.Handlers.Queries.UserBillQrs;
using Lynx.Queries.UserBillQrs;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.ViewModels
{
    [QueryProperty(nameof(BillID), nameof(BillID))]
    public class UserBillDetailViewModel : BaseViewModel
    {

        #region BillID
        private Guid billID;
        public string BillID
        {
            get => billID.ToString();
            set
            {
                billID = Guid.Parse(value);

                LoadItemId(billID);
            }
        }
        #endregion

        #region IsLoaded
        private bool isLoaded;
        public bool IsLoaded
        {
            get => isLoaded;
            set => SetProperty(ref isLoaded, value);
        }
        #endregion

        #region UserBill
        private UserBillVM userBill;
        public UserBillVM UserBill
        {
            get { return userBill; }
            set { SetProperty(ref userBill, value); }
        }
        #endregion

        #region IsPaid
        private bool isPaid;
        public bool IsPaid
        {
            get { return isPaid; }
            set { SetProperty(ref isPaid, value); }
        }
        #endregion

        public ICommand MarkAsPaidCommand { get; }




        public UserBillDetailViewModel()
        {
            MarkAsPaidCommand = new Command(MarkAsPaidCommandAsync);
        }


        private void MarkAsPaidCommandAsync()
        {
            Task.Run(() =>
            {

            });
        }

        private void LoadItemId(Guid billID)
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    UserBill = await TasqR
                        .UsingAsHandler<FindUserBillQrHandler_API>()
                        .RunAsync(new FindUserBillQr(billID));
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }

                IsLoaded = true;
                IsBusy = false;
            });
        }
    }
}