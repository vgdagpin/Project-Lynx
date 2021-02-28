using System;
using Lynx.Infrastructure.Common.Constants;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Common
{
    public class AppUser : IAppUser
    {
        private readonly ILynxDbContext p_DbContext;

        public Guid UserID { get; } = Guid.Parse(UserIDConstants.Enteng);
        public Guid SessionUID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AppUser(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }
    }
}
