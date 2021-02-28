using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserBillQrs
{
    public class FindUserBillQrHandler : TasqHandlerAsync<FindUserBillQr, UserBillVM>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        protected FindUserBillQrHandler() { }

        public FindUserBillQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public override Task<UserBillVM> RunAsync(FindUserBillQr process, CancellationToken cancellationToken = default)
        {
            return p_DbContext.UserBills
                   .Include(a => a.N_TrackBill)
                   .ThenInclude(a => a.N_Bill)
                   .SingleOrDefaultAsync(a => a.ID == process.UserBillID, cancellationToken)
                   .ContinueWith(res =>
                   {
                       if (res.Result == null)
                       {
                           return null;
                       }

                       return p_Mapper.Map<UserBillVM>(res.Result);
                   }, cancellationToken);
        }
    }
}
