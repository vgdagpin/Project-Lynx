using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Models;
using TasqR;

namespace Lynx.Queries.UserSessionQrs
{
    public class GetSessionFromTokenQr : ITasq<UserSessionBO>
    {
        public GetSessionFromTokenQr(string sessionToken)
        {
            SessionToken = sessionToken;
        }

        public string SessionToken { get; }
    }
}
