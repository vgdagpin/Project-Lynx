using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Application.Common.Extensions;
using Lynx.Commands.FirebaseTokenCmds;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using TasqR;

namespace Lynx.Application.Handlers.Commands.FirebaseTokenCmds
{
    public class SaveFirebaseTokenCmdHandler : TasqHandler<SaveFirebaseTokenCmd>
    {
        protected SaveFirebaseTokenCmdHandler() { }

        public SaveFirebaseTokenCmdHandler(ILynxDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ILynxDbContext DbContext { get; }

        public override void Run(SaveFirebaseTokenCmd request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                throw new LynxException("Token not provided");
            }

            if (!DbContext.FirebaseTokens.Any(a => a.Token == request.Token))
            {
                DbContext.FirebaseTokens.Add(new FirebaseToken
                {
                    Token = request.Token
                });

                DbContext.SaveChanges();
            }
        }
    }
}
