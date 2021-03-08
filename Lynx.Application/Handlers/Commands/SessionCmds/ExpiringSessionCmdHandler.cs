using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserSessionCmds;
using Lynx.Interfaces;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.SessionCmds
{
    public class ExpiringSessionCmdHandler : TasqHandlerAsync<ExpiringSessionCmd, bool>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly ITasqR p_TasqR;
        private readonly IDateTime p_DateTime;
        private readonly DbContext p_DbContextBase;

        public ExpiringSessionCmdHandler(ILynxDbContext dbContext, ITasqR tasqR, IDateTime dateTime)
        {
            p_DbContext = dbContext;
            p_TasqR = tasqR;
            p_DateTime = dateTime;

            p_DbContextBase = dbContext as DbContext;
        }

        public override Task<bool> RunAsync(ExpiringSessionCmd request, CancellationToken cancellationToken = default)
        {

            return p_TasqR.RunAsync(new GetSessionFromTokenQr(request.Token))
                .ContinueWith(a =>
                {
                    a.Result.ExpiredOn = p_DateTime.Now.AddHours(-1);

                    return p_DbContextBase.SaveChangesAsync().ContinueWith(b => 
                    {
                        return Task.FromResult(b.Result > 0);
                    })
                    .Unwrap();
                })
                .Unwrap();
        }
    }
}
