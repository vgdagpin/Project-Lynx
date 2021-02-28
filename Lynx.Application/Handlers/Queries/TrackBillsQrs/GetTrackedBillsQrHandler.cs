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
using Lynx.Queries.TrackBillsQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.TrackBillsQrs
{
    public class GetTrackedBillsQrHandler : TasqHandlerAsync<GetTrackBillsQr, IEnumerable<TrackBillSummaryVM>>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        protected GetTrackedBillsQrHandler() { }

        public GetTrackedBillsQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public async override Task<IEnumerable<TrackBillSummaryVM>> RunAsync(GetTrackBillsQr process, CancellationToken cancellationToken = default)
        {
            return await p_DbContext.TrackBills
                    .Include(a => a.N_Bill)
                    .Include(a => a.N_ProviderType)
                    .Include(a => a.N_TrackBillSettings)
                    .Where(a => a.UserID == process.UserID)
                    .ProjectTo<TrackBillSummaryVM>(p_Mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}
