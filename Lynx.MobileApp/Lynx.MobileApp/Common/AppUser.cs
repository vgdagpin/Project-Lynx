﻿using System;
using Lynx.Constants;
using Lynx.Interfaces;

namespace Lynx.MobileApp.Common
{
    public class AppUser : IAppUser
    {
        private readonly ILynxDbContext p_DbContext;

        public Guid UserID { get; } = Guid.Parse(UserIDConstants.Enteng);
        public Guid SessionUID { get; }

        public AppUser(ILynxDbContext dbContext)
        {
            p_DbContext = dbContext;
        }

        public T GetUserDetail<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
