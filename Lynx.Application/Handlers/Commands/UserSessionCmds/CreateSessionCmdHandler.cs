using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Application.Common.Extensions;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.SessionCmds
{
    public class CreateSessionCmdHandler : TasqHandlerAsync<CreateSessionCmd, UserSession>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly ITasqR p_TasqR;
        private readonly IDateTime p_DateTime;
        private readonly DbContext p_DbContextBase;

        public CreateSessionCmdHandler(ILynxDbContext dbContext, ITasqR tasqR, IDateTime dateTime)
        {
            p_DbContext = dbContext;
            p_TasqR = tasqR;
            p_DateTime = dateTime;
            p_DbContextBase = p_DbContext as DbContext;

        }
        public async override Task<UserSession> RunAsync(CreateSessionCmd process, CancellationToken cancellationToken = default)
        {
            var user = await p_DbContext.Users
               .Include(a => a.UserLogin)
               .Where(a => a.UserLogin.Username == process.Username)
               .SingleOrDefaultAsync();

            var newSession = new UserSession
            {
                SessionID = process.Sid,
                Status = Enums.SessionStatus.Active,
                Token = process.Token,
                ExpiredOn = process.Expiration,
                UserID = user.ID,
                CreatedOn = p_DateTime.Now
            };

            p_DbContext.UserSessions.AsDbSet()
                .Add(newSession);

            await p_DbContextBase.SaveChangesAsync();

            return newSession;
        }
    }
}
