using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class GetUserBillsQrHandler : TasqHandler<GetUserBillsQr, IQueryable<UserBillVM>>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        public GetUserBillsQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public override IQueryable<UserBillVM> Run(GetUserBillsQr process)
        {
            return p_DbContext.UserBills
                .Include(a => a.N_TrackBill)
                .ThenInclude(a => a.N_Bill)
                .Where(a => a.UserID == process.UserID)
                .ProjectTo<UserBillVM>(p_Mapper.ConfigurationProvider);
        }
    }
}
