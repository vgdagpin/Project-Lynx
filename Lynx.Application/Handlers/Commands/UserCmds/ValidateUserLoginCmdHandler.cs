using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserLoginCmds;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.UserCmds
{
    public class ValidateUserLoginCmdHandler : TasqHandlerAsync<ValidateUserLoginCmd, LoginResultVM>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IPasswordHasher p_PasswordHasher;

        public ValidateUserLoginCmdHandler(ILynxDbContext dbContext, IPasswordHasher passwordHasher)
        {
            p_DbContext = dbContext;
            p_PasswordHasher = passwordHasher;
        }

        public async override Task<LoginResultVM> RunAsync(ValidateUserLoginCmd process, CancellationToken cancellationToken = default)
        {
            var userLogin = await p_DbContext.UserLogins
                .SingleOrDefaultAsync(a => a.Username == process.Username);

            if (userLogin == null)
            {
                return new LoginResultVM
                {
                    IsSuccess = false,
                    Error = new LynxException("Username or password not found")
                };
            }

            if (!p_PasswordHasher.IsPasswordVerified(userLogin.Salt, userLogin.Password, process.Password))
            {
                return new LoginResultVM
                {
                    IsSuccess = false,
                    Error = new LynxException("Username or password not found")
                };
            }

            return new LoginResultVM
            {
                IsSuccess = true
            };
        }

        
    }
}
