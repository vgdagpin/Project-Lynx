using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Commands.EmailWorkerCmds;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using TasqR;

namespace Lynx.Application.Handlers.Commands.EmailWorkerCmds
{
    public class ReadUserBillFromGlobeEmailCmdHandler : BaseReadUserBillFromEmailCmdHandler
    {
        public ReadUserBillFromGlobeEmailCmdHandler(ILynxDbContext dbContext) : base(dbContext)
        {
            
        }

        public override UserBillVM Run(ReadUserBillFromEmailCmd request)
        {
            var emailBody = ReadEmail(request.EmailID, true);

            return new UserBillVM
            {
                Amount = 1,
                DueDate = DateTime.Now,
                LongDesc = emailBody
            };
        }
    }
}
