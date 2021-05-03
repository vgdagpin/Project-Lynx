using System;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using TasqR;

namespace Lynx.Queries.UserBillQrs
{
    /// <summary>
    /// Default handler: GetUserBillQrHandler
    /// </summary>
    public class FindUserBillQr : ITasq<UserBillBO>
    {
        public FindUserBillQr(Guid userBillID)
        {
            UserBillID = userBillID;
        }

        public Guid UserBillID { get; }
    }
}
