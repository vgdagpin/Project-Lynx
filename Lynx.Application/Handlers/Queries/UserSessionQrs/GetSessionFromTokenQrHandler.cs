using System.Threading;
using System.Threading.Tasks;
using Lynx.Application.Common.Extensions;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserSessionQrs
{
    public class GetSessionFromTokenQrHandler : TasqHandlerAsync<GetSessionFromTokenQr, UserSession>
    {
        private readonly ILynxDbContext p_DbContext;

        public GetSessionFromTokenQrHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public async override Task<UserSession> RunAsync(GetSessionFromTokenQr process, CancellationToken cancellationToken = default)
        {
            var session = await p_DbContext.UserSessions
                .AsDbSet()
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Token == process.SessionToken);

            return session;
        }
    }
}
