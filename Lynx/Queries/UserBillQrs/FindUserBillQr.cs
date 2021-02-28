using System;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.Queries.UserBillQrs
{
    /// <summary>
    /// Default handler: GetUserBillQrHandler
    /// </summary>
    public class FindUserBillQr : ITasq<UserBillVM>
    {
        public FindUserBillQr(Guid userBillID)
        {
            UserBillID = userBillID;
        }

        public Guid UserBillID { get; }
    }
}
