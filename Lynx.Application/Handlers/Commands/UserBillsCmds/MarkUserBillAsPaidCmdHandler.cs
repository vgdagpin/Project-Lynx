using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserBillsCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Commands.UserBillsCmds
{
    public class MarkUserBillAsPaidCmdHandler : TasqHandlerAsync<MarkUserBillAsPaidCmd, MarkPaidResult>
    {
        private readonly ILynxDbContext p_DbContext;
        
        private DbContext p_BaseDbContext;

        protected MarkUserBillAsPaidCmdHandler() { }

        public MarkUserBillAsPaidCmdHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public override Task InitializeAsync(MarkUserBillAsPaidCmd request, CancellationToken cancellationToken)
        {
            p_BaseDbContext = p_DbContext as DbContext;

            return base.InitializeAsync(request, cancellationToken);
        }

        public override Task<MarkPaidResult> RunAsync(MarkUserBillAsPaidCmd request, CancellationToken cancellationToken = default)
        {
            return p_DbContext.UserBills
                .SingleOrDefaultAsync(a => a.ID == request.UserBillID)
                .ContinueWith(res =>
                {
                    if (res.Result == null)
                    {
                        return Task.FromResult(new MarkPaidResult
                        {
                            Error = "Object not found"
                        });
                    }

                    res.Result.Status = BillPaymentStatus.Paid;

                    return p_BaseDbContext.SaveChangesAsync()
                        .ContinueWith(r =>
                        {
                            if (r.IsFaulted)
                            {
                                return new MarkPaidResult
                                {
                                    IsSuccess = false,
                                    Error = r.Exception.Message
                                };
                            }

                            return new MarkPaidResult
                            {
                                IsSuccess = true
                            };
                        });

                }).Unwrap();
        }
    }
}
