using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.ViewModels;
using TasqR;

namespace Lynx.Commands.UserSessionCmds
{
    public class VerifyTokenValidityCmd : ITasq<TokenVerificationResult>
    {
        public VerifyTokenValidityCmd(Guid sessionID)
        {
            SessionID = sessionID;
        }

        public Guid SessionID { get; }
    }
}
