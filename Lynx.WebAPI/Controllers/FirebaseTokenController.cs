using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Commands.FirebaseTokenCmds;
using Lynx.Domain.Entities;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class FirebaseTokenController : LynxBaseController
    {
        private readonly ILogger p_Logger;

        public FirebaseTokenController(ITasqR tasqR, IAppUser appUser, ILogger<FirebaseTokenController> logger) 
            : base(tasqR, appUser)
        {
            p_Logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("/FirebaseToken/Save")]
        public bool Save([FromBody]string token)
        {
            try
            {
                TasqR.Run(new SaveFirebaseTokenCmd(token));

                return true;
            }
            catch (Exception ex)
            {
                p_Logger.LogError(ex);

                return false;
            }
        }
    }
}
