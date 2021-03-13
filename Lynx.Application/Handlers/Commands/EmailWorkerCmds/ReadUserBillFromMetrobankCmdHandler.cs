using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Interfaces;

namespace Lynx.Application.Handlers.Commands.EmailWorkerCmds
{
    public class ReadUserBillFromMetrobankCmdHandler : BaseReadUserBillFromEmailCmdHandler
    {
        public ReadUserBillFromMetrobankCmdHandler(ILynxDbContext dbContext) : base(dbContext)
        {
        }
    }
}
