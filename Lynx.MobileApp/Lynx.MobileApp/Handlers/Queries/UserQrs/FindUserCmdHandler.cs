﻿using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserLoginCmds
{
    public class FindUserCmdHandler : TasqHandlerAsync<FindUserCmd, UserBO>
    {
        private readonly ILynxDbContext p_DbContext;

        public FindUserCmdHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public async override Task<UserBO> RunAsync(FindUserCmd process, CancellationToken cancellationToken = default)
        {
            var user = await p_DbContext.Users
                .Include(a => a.UserLogin)
                .SingleOrDefaultAsync(a => a.UserLogin.Username == process.Username);

            if (user == null)
            {
                return null;
            }

            return new UserBO
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
