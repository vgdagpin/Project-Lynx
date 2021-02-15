using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.Entities;
using Lynx.Enums;
using Lynx.Interfaces;
using Lynx.Queries.UserSessionCmds;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserSessionQrs
{
    public class GetActiveUserSessionQrHandler : TasqHandler<GetActiveUserSessionQr, UserSession>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IDateTime p_DateTime;

        public GetActiveUserSessionQrHandler(ILynxDbContext dbContext, IDateTime dateTime)
        {
            p_DbContext = dbContext;
            p_DateTime = dateTime;
        }

        public override UserSession Run(GetActiveUserSessionQr process)
        {
            var activeSession = p_DbContext.UserSessions
                .SingleOrDefault(a => a.Status == SessionStatus.Active && (a.ExpiredOn > p_DateTime.Now || a.ExpiredOn == null));

            return activeSession;
        }
    }
}
