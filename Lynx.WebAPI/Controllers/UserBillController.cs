using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class UserBillController : LynxBaseController
    {
        private readonly ILogger<UserBillController> p_Logger;

        public UserBillController(ITasqR tasqR, ILogger<UserBillController> logger, IAppUser appUser) : base(tasqR, appUser)
        {
            p_Logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserBillSummaryVM>> Get()
        {
            return await TasqR.RunAsync(new GetUserBillsQr(AppUser.UserID));
        }

        [HttpGet("{id}")]
        public async Task<UserBillVM> Get(Guid id)
        {
            return await TasqR.RunAsync(new GetUserBillQr(id));
        }
    }
}