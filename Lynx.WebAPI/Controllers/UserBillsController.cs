﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Commands.UserBillsCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class UserBillsController : LynxBaseController
    {
        private readonly ILogger<UserBillsController> p_Logger;

        public UserBillsController(ITasqR tasqR, ILogger<UserBillsController> logger, IAppUser appUser) : base(tasqR, appUser)
        {
            p_Logger = logger;
        }

        [HttpGet]
        public Task<IEnumerable<UserBillSummaryBO>> Get(int forecastDays = 30)
        {
            return TasqR.RunAsync(new GetUserBillsQr(AppUser.UserID, forecastDays));
        }

        [HttpGet("{id}")]
        public Task<UserBillBO> Get(Guid id)
        {
            return TasqR.RunAsync(new FindUserBillQr(id));
        }


        [HttpPost("{id}/MarkBillAsPaid")]
        public Task<MarkPaidResult> MarkBillAsPaid(Guid id)
        {
            return TasqR.RunAsync(new MarkUserBillAsPaidCmd(id));
        }
    }
}