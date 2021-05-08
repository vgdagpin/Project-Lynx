using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Common.Extensions;
using Lynx.Commands.UserSessionCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Enums;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserSessionCmds
{
    public class CreateSessionCmdHandler : TasqHandlerAsync<CreateSessionCmd, UserSessionBO>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IDateTime p_DateTime;
        private readonly IGuid p_Guid;
        private readonly IMapper p_Mapper;

        public CreateSessionCmdHandler(ILynxDbContext dbContext, IDateTime dateTime, IGuid guid, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_DateTime = dateTime;
            p_Guid = guid;
            p_Mapper = mapper;
        }

        public async override Task<UserSessionBO> RunAsync(CreateSessionCmd process, CancellationToken cancellationToken = default)
        {
            var user = await p_DbContext.UserLogins
                .Include(a => a.User)
                .SingleOrDefaultAsync(a => a.Username == process.Username);

            if (user == null)
            {
                throw new LynxException("User not found");
            }

            // we need to revoke active sessions to prevent multiple logins
            //TODO: this may cause performance problem in the future
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

            var newSession = new UserSessionBO
            {
                UserID = user.ID,
                CreatedOn = p_DateTime.Now,
                SessionID = p_Guid.NewGuid(),
                Status = SessionStatus.Active,
                Token = p_Guid.NewGuid().ToString("D"),
            };

            p_DbContext.UserSessions.Add(p_Mapper.Map<UserSession>(newSession));

            return newSession;
        }
    }
}
