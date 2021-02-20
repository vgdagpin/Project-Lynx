using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.Queries.UserBillQrs
{
    public class GetUserBillsQr : ITasq<IEnumerable<UserBillVM>>
    {
        public GetUserBillsQr(Guid userID)
        {
            UserID = userID;
        }

        public Guid UserID { get; }
    }
}
