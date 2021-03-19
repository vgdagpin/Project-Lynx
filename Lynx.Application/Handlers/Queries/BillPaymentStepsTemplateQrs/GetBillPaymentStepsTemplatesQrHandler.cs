using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.BillPaymentStepsTemplateQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.BillPaymentStepsTemplateQrs
{
    public class GetBillPaymentStepsTemplatesQrHandler : TasqHandlerAsync<GetBillPaymentStepsTemplatesQr, IEnumerable<BillPaymentStepsTemplateSummaryVM>>
    {
        private readonly ILynxDbContext p_DbContext;

        public GetBillPaymentStepsTemplatesQrHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public async override Task<IEnumerable<BillPaymentStepsTemplateSummaryVM>> RunAsync(GetBillPaymentStepsTemplatesQr request, CancellationToken cancellationToken = default)
        {
            IQueryable<BillPaymentStepsTemplate> query = p_DbContext.BillPaymentStepsTemplates
                .Where(a => a.BillID == request.BillID);

            if (!string.IsNullOrWhiteSpace(request.Query))
            {
                query = query
                    .Where(a => a.Title.Contains(request.Query)
                        || ("," + a.Keywords + ",").Contains("," + request.Query + ","));
            }

            return await query
                .Select(a => new BillPaymentStepsTemplateSummaryVM
                {
                    ID = a.ID,
                    Title = a.Title,
                    ShortDesc = a.ShortDesc
                })
                .ToListAsync();
        }
    }
}