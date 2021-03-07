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
    [Route("[controller]")]
    public class PingController : LynxBaseController
    {
        public PingController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {

        }

        [HttpGet]
        [AllowAnonymous]
        public string Ping() => "Pong";
    }
}
