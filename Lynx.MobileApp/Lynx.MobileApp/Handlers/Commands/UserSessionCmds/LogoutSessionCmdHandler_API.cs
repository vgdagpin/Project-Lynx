using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserSessionCmds
{
    public class LogoutSessionCmdHandler_API : TasqHandlerAsync<LogoutSessionCmd>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IAppUser p_AppUser;
        private readonly ITasqR p_TasqR;
        private readonly DbContext p_BaseDbContext;
        private readonly DbSet<UserSession> p_UserSessionDbSet;

        public LogoutSessionCmdHandler_API(ILynxDbContext dbContext, IAppUser appUser, ITasqR tasqR)
        {
            p_DbContext = dbContext;
            p_AppUser = appUser;
            p_TasqR = tasqR;

            p_BaseDbContext = dbContext as DbContext;
            p_UserSessionDbSet = (DbSet<UserSession>)dbContext.UserSessions;
        }


        public async override Task RunAsync(LogoutSessionCmd request, CancellationToken cancellationToken = default)
        {
            Thread.Sleep(2000);

            var token = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));

            if (token != null)
            {
                var s = p_UserSessionDbSet.Find(token.SessionID);

                p_UserSessionDbSet.Remove(s);
                p_BaseDbContext.SaveChanges();
            }
        }
    }
}
