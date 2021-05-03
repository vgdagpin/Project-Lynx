using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Common.Extensions;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.SessionCmds
{
    public class CreateSessionCmdHandler : TasqHandlerAsync<CreateSessionCmd, UserSessionBO>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly ITasqR p_TasqR;
        private readonly IDateTime p_DateTime;
        private readonly IMapper p_Mapper;
        private readonly DbContext p_DbContextBase;

        public CreateSessionCmdHandler(ILynxDbContext dbContext, ITasqR tasqR, IDateTime dateTime, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_TasqR = tasqR;
            p_DateTime = dateTime;
            p_Mapper = mapper;
            p_DbContextBase = p_DbContext as DbContext;

        }
        public async override Task<UserSessionBO> RunAsync(CreateSessionCmd process, CancellationToken cancellationToken = default)
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

            p_DbContext.UserSessions.Add(newSession);

            await p_DbContextBase.SaveChangesAsync();

            var result = p_Mapper.Map<UserSessionBO>(newSession);

            result.UserData = p_Mapper.Map<UserBO>(user);

            return result;
        }
    }
}
