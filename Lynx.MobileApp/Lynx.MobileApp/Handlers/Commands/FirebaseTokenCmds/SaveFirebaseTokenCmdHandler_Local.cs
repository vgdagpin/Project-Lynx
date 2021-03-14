using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Application.Handlers.Commands.FirebaseTokenCmds;
using Lynx.Commands.FirebaseTokenCmds;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Handlers.Commands.FirebaseTokenCmds
{
    public class SaveFirebaseTokenCmdHandler_Local : SaveFirebaseTokenCmdHandler
    {
        public SaveFirebaseTokenCmdHandler_Local(ILynxDbContext dbContext) : base(dbContext)
        {

        }
    }
}
