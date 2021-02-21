using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.BillsQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.BillsQrs
{
    public class GetBillsQrHandler : TasqHandlerAsync<GetBillsQr, IEnumerable<BillSummaryVM>>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        public GetBillsQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public async override Task<IEnumerable<BillSummaryVM>> RunAsync(GetBillsQr process, CancellationToken cancellationToken = default)
        {
            return await p_DbContext.Bills
                   .Include(a => a.N_BillSettings)
                   .Where(a => a.IsEnabled)
                   .ProjectTo<BillSummaryVM>(p_Mapper.ConfigurationProvider)
                   .ToListAsync();
        }
    }
}