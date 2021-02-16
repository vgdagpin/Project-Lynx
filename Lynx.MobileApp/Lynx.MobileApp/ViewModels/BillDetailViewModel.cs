using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    [QueryProperty(nameof(BillID), nameof(BillID))]
    public class BillDetailViewModel : BaseViewModel
    {
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


        async void LoadItemId(Guid billID)
        {
            //try
            //{
            //    var item = await DataStore.GetItemAsync(itemId);
            //    Id = item.Id;
            //    Text = item.Text;
            //    Description = item.Description;
            //}
            //catch (Exception)
            //{
            //    Debug.WriteLine("Failed to Load Item");
            //}
        }
    }
}
