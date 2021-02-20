using System;
using Lynx.Infrastructure.Common.Constants;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Common
{
    public class AppUser : IAppUser
    {
        private readonly ILynxDbContext p_DbContext;

        public Guid UserID { get; } = Guid.Parse(UserIDConstants.Enteng);

        public AppUser(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }
    }
}
