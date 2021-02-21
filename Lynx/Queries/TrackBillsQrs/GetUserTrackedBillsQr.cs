using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.TrackBillsQrs
{
    public class GetUserTrackedBillsQr : ITasq<IEnumerable<TrackBillSummaryVM>>
    {
        public GetUserTrackedBillsQr(Guid userID)
        {
            UserID = userID;
        }

        public Guid UserID { get; }
    }
}
