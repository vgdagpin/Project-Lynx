using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Lynx.Common.ViewModels;
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


        public HomePageViewModel()
        {
            tasqR = DependencyService.Resolve<ITasqR>();
            bills = tasqR.Run(new GetBillsForThisMonthQr());

            Bills = new ObservableCollection<BillVM>(bills);
        }
    }
}
