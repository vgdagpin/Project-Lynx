using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.TrackBillsQrs
{
    public class FindTrackBillQr : ITasq<TrackBillVM>
    {
        public FindTrackBillQr(Guid trackBillID)
        {
            TrackBillID = trackBillID;
        }

        public Guid TrackBillID { get; }
    }
}
