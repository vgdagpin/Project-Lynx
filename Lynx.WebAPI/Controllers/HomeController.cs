using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class HomeController : LynxBaseController
    {
        public HomeController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [AllowAnonymous]
        [HttpGet("/")]
        public string Get()
        {
            var version = Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

            return $"Yup! I'm online! Version: {version}";
        }
    }
}
