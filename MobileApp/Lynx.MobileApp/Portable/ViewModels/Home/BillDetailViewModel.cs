using System;
using System.Threading.Tasks;
using Lynx.Common.ViewModels;
using Lynx.Queries.UserBillQrs;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.ViewModels
{
    [QueryProperty(nameof(BillID), nameof(BillID))]
    public class BillDetailViewModel : BaseViewModel
    {
        private readonly ITasqR p_TasqR;

        private Guid billID;
        public string BillID
        {
            get { return billID.ToString(); }
            set
            {
                billID = Guid.Parse(value);

                LoadItemId(billID);
            }
        }

        private bool isLoaded;
        public bool IsLoaded
        {
            get { return isLoaded; }
            set { SetProperty(ref isLoaded, value); }
        }

        private UserBillVM userBill;
        public UserBillVM UserBill
        {
            get { return userBill; }
            set { SetProperty(ref userBill, value); }
        }


        public BillDetailViewModel()
        {
            p_TasqR = App.ServiceProvider.GetService<ITasqR>();
        }

        private Task LoadItemId(Guid billID)
        {
            return Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    UserBill = await p_TasqR.RunAsync(new GetUserBillQr(billID));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogError(ex);
                }

                IsLoaded = true;
                IsBusy = false;
            });
        }
    }
}