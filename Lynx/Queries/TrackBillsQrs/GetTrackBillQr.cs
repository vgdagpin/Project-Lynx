using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.TrackBillsQrs
{
    public class GetTrackBillQr : ITasq<TrackBillVM>
    {
        public GetTrackBillQr(Guid trackBillID)
        {
            TrackBillID = trackBillID;
        }

        public Guid TrackBillID { get; }
    }
}
