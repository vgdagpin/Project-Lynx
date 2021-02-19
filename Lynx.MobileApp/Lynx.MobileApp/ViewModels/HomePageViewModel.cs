using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Infrastructure.Common.Constants;
using Lynx.MobileApp.Views;
using Lynx.Queries.BillsQrs;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
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
        public Command LoadData { get; }

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

        async void loadDataCommand()
        {
            var bills = tasqR.Run(new GetUserBillsQr(Guid.Parse(UserIDConstants.Enteng)));

            foreach (var bill in bills)
            {
                Bills.Add(bill);
            }            
        }
    }
}
