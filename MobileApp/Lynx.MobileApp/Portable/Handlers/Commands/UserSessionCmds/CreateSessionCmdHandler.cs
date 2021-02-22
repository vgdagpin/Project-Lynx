using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.Entities;
using Lynx.Enums;
using Lynx.Infrastructure.Common.Extensions;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserSessionCmds
{
    public class CreateSessionCmdHandler : TasqHandlerAsync<CreateSessionCmd, UserSession>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IDateTime p_DateTime;
        private readonly IGuid p_Guid;

        public CreateSessionCmdHandler(ILynxDbContext dbContext, IDateTime dateTime, IGuid guid)
        {
            p_DbContext = dbContext;
            p_DateTime = dateTime;
            p_Guid = guid;
        }

        public async override Task<UserSession> RunAsync(CreateSessionCmd process, CancellationToken cancellationToken = default)
        {
            var user = await p_DbContext.UserLogins
                .Include(a => a.User)
                .SingleOrDefaultAsync(a => a.Username == process.Username);

            if (user == null)
            {
                throw new LynxException("User not found");
            }

            // we need to revoke active sessions to prevent multiple logins
            p_DbContext.UserSessions
                .Where(a => a.Status == SessionStatus.Active 
                    && a.ExpiredOn >= p_DateTime.Now 
                    && a.UserID == user.ID)
                .ToList()
                .ForEach(session =>
                {
                    session.Status = SessionStatus.Revoked;
                    session.Remarks = "Forced expired to prevent multiple login";
                });

            var newSession = new UserSession
            {
                UserID = user.ID,
                CreatedOn = p_DateTime.Now,
                SessionID = p_Guid.NewGuid(),
                Status = SessionStatus.Active,
                Token = p_Guid.NewGuid().ToString("D"),
            };

            p_DbContext.UserSessions.AsDbSet().Add(newSession);

            return newSession;
        }
    }
}
