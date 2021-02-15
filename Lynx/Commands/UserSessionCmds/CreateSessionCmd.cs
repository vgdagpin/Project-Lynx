using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using TasqR;

namespace Lynx.Commands.UserSessionCmds
{
    public class CreateSessionCmd : ITasq<UserSession>
    {
        public CreateSessionCmd(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}
