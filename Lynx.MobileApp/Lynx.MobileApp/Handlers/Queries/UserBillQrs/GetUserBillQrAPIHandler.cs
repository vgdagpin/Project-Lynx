using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class GetUserBillQrAPIHandler : TasqHandlerAsync<GetUserBillQr, UserBillVM>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        public GetUserBillQrAPIHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public async override Task<UserBillVM> RunAsync(GetUserBillQr process, CancellationToken cancellationToken = default)
        {
            var result = await p_DbContext.UserBills
                   .Include(a => a.N_TrackBill)
                   .ThenInclude(a => a.N_Bill)
                   .SingleOrDefaultAsync(a => a.UserID == process.UserBillID, cancellationToken);

            if (result == null)
            {
                return null;
            }

            return p_Mapper.Map<UserBillVM>(result);
        }
    }
}
