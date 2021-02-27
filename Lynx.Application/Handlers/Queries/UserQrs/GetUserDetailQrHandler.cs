using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.UserQrs
{
    public class GetUserDetailQrHandler : TasqHandlerAsync<GetUserDetailQr, User>
    {
        private readonly ILynxDbContext p_DbContext;

        public GetUserDetailQrHandler(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public async override Task<User> RunAsync(GetUserDetailQr process, CancellationToken cancellationToken = default)
        {
            var user = await p_DbContext.Users
                .Include(a => a.UserLogin)
                .SingleOrDefaultAsync(a => a.UserLogin.Username == process.EmailOrUserName);

            return user;
        }
    }
}
