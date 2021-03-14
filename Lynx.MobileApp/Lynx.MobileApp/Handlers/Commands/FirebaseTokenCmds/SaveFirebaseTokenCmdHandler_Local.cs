using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lynx.Application.Common.Extensions;
using Lynx.Application.Handlers.Commands.FirebaseTokenCmds;
using Lynx.Commands.FirebaseTokenCmds;
using Lynx.Domain.Entities;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Handlers.Commands.FirebaseTokenCmds
{
    public class SaveFirebaseTokenCmdHandler_Local : SaveFirebaseTokenCmdHandler
    {
        public SaveFirebaseTokenCmdHandler_Local(ILynxDbContext dbContext) : base(dbContext)
        {

        }

        public override void Run(SaveFirebaseTokenCmd request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                throw new LynxException("Token not provided");
            }

            if (DbContext.FirebaseTokens.Any())
            {
                DbContext.FirebaseTokens.ToList()
                    .ForEach(t =>
                    {
                        DbContext.FirebaseTokens.Remove(t);
                    });
            }

            DbContext.FirebaseTokens.Add(new FirebaseToken
            {
                Token = request.Token
            });

            DbContext.SaveChanges();
        }
    }
}
