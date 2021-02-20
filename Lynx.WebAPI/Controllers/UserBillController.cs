using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserBillController : ControllerBase
    {
        private readonly ITasqR p_TasqR;
        private readonly ILogger<UserBillController> p_Logger;
        private readonly IAppUser p_AppUser;

        public UserBillController(ITasqR tasqR, ILogger<UserBillController> logger, IAppUser appUser)
        {
            p_TasqR = tasqR;
            p_Logger = logger;
            p_AppUser = appUser;
        }

        [HttpGet]
        public async Task<IEnumerable<UserBillVM>> Get()
        {
            return await p_TasqR.RunAsync(new GetUserBillsQr(p_AppUser.UserID));
        }

        [HttpGet("{id}")]
        public async Task<UserBillVM> Get(Guid id)
        {
            return await p_TasqR.RunAsync(new GetUserBillQr(id));
        }
    }
}