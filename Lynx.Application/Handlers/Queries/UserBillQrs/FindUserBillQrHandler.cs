using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserBillQrs
{
    public class FindUserBillQrHandler : TasqHandlerAsync<FindUserBillQr, UserBillBO>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        protected FindUserBillQrHandler() { }

        public FindUserBillQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public async override Task<UserBillBO> RunAsync(FindUserBillQr process, CancellationToken cancellationToken = default)
        {
            var result = await p_DbContext.UserBills
                   .Include(a => a.N_TrackBill)
                   .ThenInclude(a => a.N_Bill)
                   .SingleOrDefaultAsync(a => a.ID == process.UserBillID, cancellationToken);

            if (result == null)
            {
                return null;
            }

            return p_Mapper.Map<UserBillBO>(result);
        }
    }
}
