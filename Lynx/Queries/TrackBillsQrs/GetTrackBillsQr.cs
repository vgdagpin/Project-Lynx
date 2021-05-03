using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.TrackBillsQrs
{
    public class GetTrackBillsQr : ITasq<IEnumerable<TrackBillSummaryBO>>
    {
        public GetTrackBillsQr(Guid userID)
        {
            UserID = userID;
        }

        public Guid UserID { get; }
    }
}
