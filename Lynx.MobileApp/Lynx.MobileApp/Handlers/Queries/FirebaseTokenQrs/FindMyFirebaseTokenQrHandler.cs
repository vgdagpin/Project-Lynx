using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Interfaces;
using Lynx.Queries.FirebaseTokenCmds;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.FirebaseTokenQrs
{
    public class FindMyFirebaseTokenQrHandler : TasqHandler<FindMyFirebaseTokenQr, string>
    {
        private readonly ILynxDbContext p_DbCOntext;

        public FindMyFirebaseTokenQrHandler(ILynxDbContext dbCOntext)
        {
            p_DbCOntext = dbCOntext;
        }

        public override string Run(FindMyFirebaseTokenQr request)
        {
            return p_DbCOntext.FirebaseTokens
                .SingleOrDefault()?
                .Token;
        }
    }
}
