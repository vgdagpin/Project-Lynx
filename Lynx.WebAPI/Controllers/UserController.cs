using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class UserController : LynxBaseController
    {
        public UserController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet]
        public Task<UserVM> Get()
        {
            return TasqR.RunAsync(new GetUserDetailQr(AppUser.UserID));
        }
    }
}
