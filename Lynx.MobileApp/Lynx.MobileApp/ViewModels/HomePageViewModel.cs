using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.Infrastructure.Common.Constants;
using Lynx.MobileApp.Views;
using Lynx.Queries.UserBillQrs;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        protected readonly ITasqR tasqR;
        public ObservableCollection<UserBillVM> Bills { get; protected set; }

        public Command<BillVM> ItemTapped { get; }
        public ICommand LoadData { get; }

        public HomePageViewModel()
        {
            tasqR = App.ServiceProvider.GetService<ITasqR>();

            Bills = new ObservableCollection<UserBillVM>();

            ItemTapped = new Command<BillVM>(navigateBillCommand);
            LoadData = new Command(loadDataCommand);
        }

        async void navigateBillCommand(BillVM bill)
        {
            await Shell.Current.GoToAsync($"{nameof(BillDetail)}?{nameof(BillDetailViewModel.BillID)}={bill.ID}");
        }

        bool isLoading = false;
        async void loadDataCommand()
        {
            if (isLoading)
            {
                return;
            }

            Bills.Clear();

            try
            {
                isLoading = true;
                var qr = new GetUserBillsQr(Guid.Parse(UserIDConstants.Enteng));
                var bills = await tasqR.RunAsync(qr);

                foreach (var item in bills)
                {
                    Bills.Add(item);
                }

                isLoading = false;
            }
            catch (Exception ex)
            {
                isLoading = false;
            }
        }
    }
}
