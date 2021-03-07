using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.UserSessionCmds
{
    public class VerifyTokenValidityCmdHandler : TasqHandlerAsync<VerifyTokenValidityCmd, TokenVerificationResult>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IDateTime p_DateTime;

        public VerifyTokenValidityCmdHandler(ILynxDbContext dbContext, IDateTime dateTime)
        {
            p_DbContext = dbContext;
            p_DateTime = dateTime;
        }

        public async override Task<TokenVerificationResult> RunAsync(VerifyTokenValidityCmd request, CancellationToken cancellationToken = default)
        {           
            var session = await p_DbContext.UserSessions.SingleOrDefaultAsync(a => a.SessionID == request.SessionID);

            if (session == null)
            {
                return new TokenVerificationResult
                {
                    TokenStatus = TokenStatus.Invalid
                };
            }

            if (session.ExpiredOn <= p_DateTime.Now || session.IsExpired)
            {
                return new TokenVerificationResult
                {
                    Expiration = session.ExpiredOn,
                    TokenStatus = TokenStatus.Expired
                };
            }

            return new TokenVerificationResult
            {
                TokenStatus = TokenStatus.Active,
                Expiration = session.ExpiredOn
            };
        }
    }
}
