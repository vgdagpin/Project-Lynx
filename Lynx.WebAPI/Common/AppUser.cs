using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Infrastructure.Common.Constants;
using Lynx.Interfaces;

namespace Lynx.WebAPI.Common
{
    public class AppUser : IAppUser
    {
        public Guid UserID { get; } = Guid.Parse(UserIDConstants.Enteng);
    }
}
