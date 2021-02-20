using System;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.Queries.UserBillQrs
{
    public class GetUserBillQr : ITasq<UserBillVM>
    {
        public GetUserBillQr(Guid userBillID)
        {
            UserBillID = userBillID;
        }

        public Guid UserBillID { get; }
    }
}
