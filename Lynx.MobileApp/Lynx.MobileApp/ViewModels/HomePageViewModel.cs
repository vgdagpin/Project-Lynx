using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.MobileApp.Views;
using Lynx.Queries.BillsQrs;
using TasqR;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        protected readonly ITasqR tasqR;
        private IEnumerable<BillVM> bills = null;
        public ObservableCollection<BillVM> Bills { get; protected set; }

        public Command<BillVM> ItemTapped { get; }

        public HomePageViewModel()
        {
            tasqR = DependencyService.Resolve<ITasqR>();
            bills = tasqR.Run(new GetBillsForThisMonthQr());

            Bills = new ObservableCollection<BillVM>(bills);

            ItemTapped = new Command<BillVM>(navigateBillCommand);
        }

        async void navigateBillCommand(BillVM bill)
        {
            await Shell.Current.GoToAsync($"{nameof(BillDetail)}?{nameof(BillDetailViewModel.BillID)}={bill.ID}");
        }
    }
}
