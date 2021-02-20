using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class GetUserBillsQrHandler : TasqHandlerAsync<GetUserBillsQr, IQueryable<UserBillVM>>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        public GetUserBillsQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public override Task<IQueryable<UserBillVM>> RunAsync(GetUserBillsQr process, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(p_DbContext.UserBills
                    .Include(a => a.N_TrackBill)
                    .ThenInclude(a => a.N_Bill)
                    .Where(a => a.UserID == process.UserID)
                    .ProjectTo<UserBillVM>(p_Mapper.ConfigurationProvider));
        }
    }
}