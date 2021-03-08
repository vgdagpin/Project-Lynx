using System;
using System.Collections.Generic;
using System.Text;
using TasqR;

namespace Lynx.Commands.UserSessionCmds
{
    public class ExpiringSessionCmd : ITasq<bool>
    {
        public ExpiringSessionCmd(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
