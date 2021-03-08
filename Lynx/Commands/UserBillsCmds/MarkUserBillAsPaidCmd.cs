using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Commands.UserBillsCmds
{
    public class MarkUserBillAsPaidCmd : ITasq<MarkPaidResult>
    {
        public MarkUserBillAsPaidCmd(Guid userBillID)
        {
            UserBillID = userBillID;
        }

        public Guid UserBillID { get; }
    }
}
