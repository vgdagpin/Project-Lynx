using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Queries.BillsQrs;
using Lynx.Queries.TrackBillsQrs;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels.Manage
{
    public class NewTrackBillViewModel : BaseViewModel
    {
        public ObservableCollection<BillSummaryVM> Bills { get; protected set; } = new ObservableCollection<BillSummaryVM>();

        private BillSummaryVM selectedBill;
        public BillSummaryVM SelectedBill
        {
            get { return selectedBill; }
            set { SetProperty(ref selectedBill, value); }
        }

        private string shortDesc;
        public string ShortDesc
        {
            get { return shortDesc; }
            set { SetProperty(ref shortDesc, value); }
        }

        public ICommand OnAddEntryClicked { get; }

        public NewTrackBillViewModel()
        {
            OnAddEntryClicked = new Command(() =>
            {
                Bills.Add(new BillSummaryVM
                {
                    ShortDesc = Guid.NewGuid().ToString().Substring(0, 5)
                });
            });

            Bills.Add(new BillSummaryVM
            {
                ShortDesc = "Aw"
            });

            Bills.Add(new BillSummaryVM
            {
                ShortDesc = "Ew"
            });
            //LoadBills();
        }

        private void LoadBills()
        {
            Task.Run(() =>
            {
                try
                {
                    IsBusy = true;

                    TasqR.RunAsync(new GetBillsQr())
                        .ContinueWith(bills =>
                        {
                            //-Bills.Clear();

                            foreach (var item in bills.Result)
                            {
                                Bills.Add(item);
                            }

                            IsBusy = false;
                        });
                }
                catch (Exception ex)
                {
                    ExceptionHandler.LogError(ex);
                }

                IsBusy = false;
            });
        }
    }
}
