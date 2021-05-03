using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Common.Extensions;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserSessionQrs
{
    public class GetSessionFromTokenQrHandler : TasqHandlerAsync<GetSessionFromTokenQr, UserSessionBO>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        public GetSessionFromTokenQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public async override Task<UserSessionBO> RunAsync(GetSessionFromTokenQr process, CancellationToken cancellationToken = default)
        {
            var session = await p_DbContext.UserSessions
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Token == process.SessionToken);

            return p_Mapper.Map<UserSessionBO>(session);
        }
    }
}
