using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using TasqR;

namespace Lynx.Commands.EmailWorkerCmds
{
    public class ReadUserBillFromEmailCmd : ITasq<UserBillBO>
    {
        public ReadUserBillFromEmailCmd(long emailID)
        {
            EmailID = emailID;
        }

        public long EmailID { get; }
    }
}
