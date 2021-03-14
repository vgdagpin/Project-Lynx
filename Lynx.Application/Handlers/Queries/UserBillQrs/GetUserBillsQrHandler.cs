using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Common.ViewModels;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserBillQrs
{
    public class GetUserBillsQrHandler : TasqHandlerAsync<GetUserBillsQr, IEnumerable<UserBillSummaryVM>>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;
        private readonly IDateTime p_DateTime;

        protected GetUserBillsQrHandler() { }

        public GetUserBillsQrHandler(ILynxDbContext dbContext, IMapper mapper, IDateTime dateTime)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
            p_DateTime = dateTime;
        }

        public async override Task<IEnumerable<UserBillSummaryVM>> RunAsync(GetUserBillsQr process, CancellationToken cancellationToken = default)
        {
            DateTime maxDate = p_DateTime.Now.AddDays(process.ForecastDays);

            return await p_DbContext.UserBills
                    .Include(a => a.N_TrackBill)
                    .ThenInclude(a => a.N_Bill)
                    .Where(a => a.UserID == process.UserID && a.Status != BillPaymentStatus.Paid && a.DueDate <= maxDate)
                    .OrderByDescending(a => a.DueDate)
                    .ProjectTo<UserBillSummaryVM>(p_Mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}
