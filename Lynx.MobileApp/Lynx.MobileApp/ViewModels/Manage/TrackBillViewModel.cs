using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    [QueryProperty(nameof(TrackBillID), nameof(TrackBillID))]
    public class TrackBillViewModel : BaseViewModel
    {
        private Guid trackBillID;
        public string TrackBillID
        {
            get { return trackBillID.ToString(); }
            set
            {
                trackBillID = Guid.Parse(value);

                //LoadItemId(billID);
            }
        }
    }
}
