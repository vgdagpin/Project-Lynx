using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.BillsQrs;
using Lynx.Queries.UserBillQrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasqR;


namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class BillController : LynxBaseController
    {
        public BillController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<BillSummaryVM>> Get(CancellationToken cancellationToken = default)
        {
            return await TasqR.RunAsync(new GetBillsQr(), cancellationToken);
        }
    }
}
