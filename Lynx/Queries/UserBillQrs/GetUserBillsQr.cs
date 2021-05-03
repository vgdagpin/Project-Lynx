using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Queries.UserBillQrs
{
    public class GetUserBillsQr : ITasq<IEnumerable<UserBillSummaryBO>>
    {
        public GetUserBillsQr(Guid userID, int forecastDays)
        {
            UserID = userID;
            ForecastDays = forecastDays;
        }

        public Guid UserID { get; }
        public int ForecastDays { get; }
    }
}
