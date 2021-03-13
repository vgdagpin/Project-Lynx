using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Interfaces;

namespace Lynx.Application.Handlers.Commands.EmailWorkerCmds
{
    public class ReadUserBillFromBDOCmdHandler : BaseReadUserBillFromEmailCmdHandler
    {
        public ReadUserBillFromBDOCmdHandler(ILynxDbContext dbContext) : base(dbContext)
        {
        }
    }
}
