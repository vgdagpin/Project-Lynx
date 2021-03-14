using System;
using System.Collections.Generic;
using System.Text;
using TasqR;

namespace Lynx.Commands.FirebaseTokenCmds
{
    public class SaveFirebaseTokenCmd : ITasq
    {
        public SaveFirebaseTokenCmd(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
}
