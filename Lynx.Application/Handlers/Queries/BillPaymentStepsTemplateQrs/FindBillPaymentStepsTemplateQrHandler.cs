using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.BillPaymentStepsTemplateQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.BillPaymentStepsTemplateQrs
{
    public class FindBillPaymentStepsTemplateQrHandler : TasqHandlerAsync<FindBillPaymentStepsTemplateQr, BillPaymentStepsTemplateVM>
    {
        private readonly ILynxDbContext p_DbContext;

        public FindBillPaymentStepsTemplateQrHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public async override Task<BillPaymentStepsTemplateVM> RunAsync(FindBillPaymentStepsTemplateQr request, CancellationToken cancellationToken = default)
        {
            return await p_DbContext.BillPaymentStepsTemplates
                .Where(a => a.ID == request.BillPaymentStepsTemplateID)
                .Select(a => new BillPaymentStepsTemplateVM
                {
                    ID = a.ID,
                    Keywords = a.Keywords,
                    LongDesc = a.LongDesc,
                    ShortDesc = a.ShortDesc,
                    Title = a.Title
                })
                .SingleOrDefaultAsync();

        }
    }
}
