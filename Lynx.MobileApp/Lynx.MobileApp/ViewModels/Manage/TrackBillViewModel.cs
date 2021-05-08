using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Handlers.Queries.TrackBillsQrs;
using Lynx.Queries.TrackBillsQrs;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    [QueryProperty(nameof(TrackBillID), nameof(TrackBillID))]
    public class TrackBillViewModel : LynxViewModel
    {
        #region IsLoaded
        private bool isLoaded;
        public bool IsLoaded
        {
            get => isLoaded;
            set => SetProperty(ref isLoaded, value);
        }
        #endregion

        #region TrackBill
        private TrackBillBO trackBill;
        public TrackBillBO TrackBill
        {
            get => trackBill;
            set => SetProperty(ref trackBill, value);
        }
        #endregion

        #region TrackBillJson
        private string trackBillJson;
        public string TrackBillJson
        {
            get => trackBillJson;
            set => SetProperty(ref trackBillJson, value);
        }
        #endregion

        #region TrackBillID
        private Guid trackBillID;
        public string TrackBillID
        {
            get { return trackBillID.ToString(); }
            set
            {
                trackBillID = Guid.Parse(value);

                LoadItemId(trackBillID);
            }
        }
        #endregion

        private void LoadItemId(Guid trackBillID)
        {
            IsBusy = true;
            IsLoaded = false;

            Task.Run(async () =>
            {
                try
                {

                    TrackBill = await TasqR
                        .UsingAsHandler<FindTrackBillQrHandler_API>()
                        .RunAsync(new FindTrackBillQr(trackBillID));

                    TrackBillJson = JsonConvert.SerializeObject(TrackBill, Formatting.Indented);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }

                IsLoaded = true;
                IsBusy = false;
            });
        }
    }
}
