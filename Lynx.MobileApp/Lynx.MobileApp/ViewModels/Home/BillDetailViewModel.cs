using System;
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
            get
            {
                return billID.ToString();
            }
            set
            {
                billID = Guid.Parse(value);
                LoadItemId(billID);
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }


        public BillDetailViewModel()
        {
            p_TasqR = App.ServiceProvider.GetService<ITasqR>();
        }

        async void LoadItemId(Guid billID)
        {
            try
            {
                var item = await p_TasqR.RunAsync(new GetUserBillQr(billID));

                Name = item.LongDesc;
            }
            catch (Exception ex)
            {
                ExceptionHandler.LogError(ex);
            }
        }
    }
}
