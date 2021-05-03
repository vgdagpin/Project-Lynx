using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Application.Common.Extensions;
using Lynx.Commands.TrackBillCmds;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.TrackBillsCmds
{
    public class DeleteTrackBillCmdHandler : TasqHandlerAsync<DeleteTrackBillCmd>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IAppUser p_AppUser;
        private DbContext p_BaseDbContext;

        protected DeleteTrackBillCmdHandler() { }

        public DeleteTrackBillCmdHandler(ILynxDbContext dbContext, IAppUser appUser)
        {
            p_DbContext = dbContext;
            p_AppUser = appUser;
        }

        public override Task InitializeAsync(DeleteTrackBillCmd tasq, CancellationToken cancellationToken = default)
        {
            p_BaseDbContext = p_DbContext as DbContext;

            return base.InitializeAsync(tasq, cancellationToken);
        }


        public override Task RunAsync(DeleteTrackBillCmd request, CancellationToken cancellationToken = default)
        {
            return p_DbContext.TrackBills
                .SingleOrDefaultAsync(a => a.ID == request.Id && a.UserID == p_AppUser.UserID)
                .ContinueWith(r =>
                {
                    if (r.Result != null)
                    {
                        p_DbContext.TrackBills.Remove(r.Result);
                    }

                    return p_BaseDbContext.SaveChangesAsync()
                        .ContinueWith(a =>
                        {
                            if (a.IsFaulted)
                            {
                                throw new LynxException(a.Exception);
                            }
                        });

                }).Unwrap();
        }
    }
}
