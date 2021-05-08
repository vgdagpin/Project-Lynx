using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Portable.Handlers.Queries.UserSessionQrs
{
    public class GetTokenCmdHandler : TasqHandlerAsync<GetTokenCmd, UserSessionBO>
    {
        private readonly DbContext p_BaseDbContext;
        private readonly IMapper p_Mapper;
        private readonly DbSet<UserSession> p_UserSessionDbSet;


        public GetTokenCmdHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_Mapper = mapper;
            p_BaseDbContext = dbContext as DbContext;
            p_UserSessionDbSet = dbContext.UserSessions as DbSet<UserSession>;

        }
        public async override Task<UserSessionBO> RunAsync(GetTokenCmd request, CancellationToken cancellationToken = default)
        {
            // if more than one session found it means its broken and needs to be cleared
            if (await p_UserSessionDbSet.CountAsync() > 1)
            {
                p_UserSessionDbSet.RemoveRange(p_UserSessionDbSet);
                await p_BaseDbContext.SaveChangesAsync();
            }

            // get current saved session from the device 
            var currentSession = await p_UserSessionDbSet.SingleOrDefaultAsync();

            if (currentSession == null)
            {
                return null;
            }

            return p_Mapper.Map<UserSessionBO>(currentSession);
        }
    }
}
