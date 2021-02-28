using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using TasqR;

namespace Lynx.Queries.UserSessionQrs
{
    public class GetSessionFromTokenQr : ITasq<UserSession>
    {
        public GetSessionFromTokenQr(string sessionToken)
        {
            SessionToken = sessionToken;
        }

        public string SessionToken { get; }
    }
}
