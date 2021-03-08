using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class PingController : LynxBaseController
    {
        private readonly ILogger p_Logger;

        public PingController(ITasqR tasqR, IAppUser appUser, ILogger<PingController> logger) : base(tasqR, appUser)
        {
            p_Logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public string Ping()
        {
            p_Logger.LogInformation("Log Information Pong");
            p_Logger.LogError("Log Error Pong");
            p_Logger.LogDebug("Log Debug Pong");
            p_Logger.LogTrace("Log Trace Pong");
            p_Logger.LogWarning("Log Warning Pong");

            return "Pong";
        }
    }
}
