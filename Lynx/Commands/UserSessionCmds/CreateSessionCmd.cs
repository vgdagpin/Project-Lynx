using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using TasqR;

namespace Lynx.Commands.UserSessionCmds
{
    public class CreateSessionCmd : ITasq<UserSession>
    {
        public CreateSessionCmd(Guid sid, string username, string token, DateTime? expiration = default)
        {
            Sid = sid;
            Username = username;
            Token = token;
            Expiration = expiration;
        }

        public Guid Sid { get; }
        public string Username { get; }
        public string Token { get; }
        public DateTime? Expiration { get; }
    }
}
