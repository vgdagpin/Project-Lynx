using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Lynx.MobileApp.Views;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class ManageBillViewModel : BaseViewModel
    {
        public ICommand TrackNewBill { get; }

        public ManageBillViewModel()
        {
            TrackNewBill = new Command(TrackNewBillCommand);
        }

        async void TrackNewBillCommand()
        {
            await Shell.Current.GoToAsync($"{nameof(TrackBillPage)}");
        }
    }
}
