using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Common.ViewModels;
using TasqR;

namespace Lynx.Commands.UserCmds
{
    public class FindUserCmd : ITasq<UserVM>
    {
        public FindUserCmd(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}
