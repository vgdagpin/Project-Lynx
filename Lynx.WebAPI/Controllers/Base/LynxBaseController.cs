using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    public class LynxBaseController : ControllerBase
    {
        public LynxBaseController(ITasqR tasqR, IAppUser appUser)
        {
            TasqR = tasqR;
            AppUser = appUser;
        }

        protected ITasqR TasqR { get; }
        protected IAppUser AppUser { get; }
    }
}
