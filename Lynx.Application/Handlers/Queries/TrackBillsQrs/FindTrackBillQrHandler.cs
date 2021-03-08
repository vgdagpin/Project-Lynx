using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.TrackBillsQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.TrackBillsQrs
{
    public class FindTrackBillQrHandler : TasqHandler<FindTrackBillQr, TrackBillVM>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        public FindTrackBillQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public override TrackBillVM Run(FindTrackBillQr process)
        {
            var result = p_DbContext.TrackBills
                    .Include(a => a.N_Bill)
                    .Include(a => a.N_ProviderType)
                    .Include(a => a.N_TrackBillSettings)
                    .Include(a => a.N_ProviderTypeConfigEmail)
                    .Include(a => a.N_ProviderTypeConfigScheduler)
                    .Include(a => a.N_ProviderTypeConfigWebService)
                    .SingleOrDefault(a => a.ID == process.TrackBillID);

            return p_Mapper.Map<TrackBillVM>(result);
        }
    }
}