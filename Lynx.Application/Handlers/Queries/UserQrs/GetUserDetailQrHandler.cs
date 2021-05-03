using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserQrs
{
    public class GetUserDetailQrHandler : TasqHandlerAsync<GetUserDetailQr, UserBO>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IMapper p_Mapper;

        protected GetUserDetailQrHandler() { }

        public GetUserDetailQrHandler(ILynxDbContext dbContext, IMapper mapper)
        {
            p_DbContext = dbContext;
            p_Mapper = mapper;
        }

        public override Task<UserBO> RunAsync(GetUserDetailQr process, CancellationToken cancellationToken = default)
        {
            return p_DbContext.Users
                .Include(a => a.UserLogin)
                .Where(a => process.Id != null ? a.ID == process.Id : a.UserLogin.Username == process.EmailOrUserName)
                .ProjectTo<UserBO>(p_Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}