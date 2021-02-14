using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserLoginCmds;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserLoginCmds
{
    public class ValidateUserLoginCmdHandler : TasqHandlerAsync<ValidateUserLoginCmd, LoginResultVM>
    {


        public override Task<LoginResultVM> RunAsync(ValidateUserLoginCmd process, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new LoginResultVM
            {
                IsSuccess = true
            });
        }
    }
}
