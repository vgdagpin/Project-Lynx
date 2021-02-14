using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.Commands.UserLoginCmds
{
    public class ValidateUserLoginCmd : ITasq<LoginResultVM>
    {
        public ValidateUserLoginCmd(string username, string password)
        {

        }
    }
}
