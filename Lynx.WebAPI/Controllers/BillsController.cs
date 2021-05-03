using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.BillPaymentStepsTemplateQrs;
using Lynx.Queries.BillsQrs;
using Lynx.Queries.UserBillQrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasqR;


namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class BillsController : LynxBaseController
    {
        public BillsController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<BillSummaryBO>> Get(CancellationToken cancellationToken = default)
        {
            return await TasqR.RunAsync(new GetBillsQr(), cancellationToken);
        }

        [HttpGet("/Bills/{billID}/PaymentStepsTemplate")]
        public Task<IEnumerable<BillPaymentStepsTemplateSummaryVM>> Search(short billID, string query, CancellationToken cancellationToken = default)
        {
            return TasqR.RunAsync(new GetBillPaymentStepsTemplatesQr(billID, query), cancellationToken);
        }

        [HttpGet("/Bills/PaymentStepsTemplate/{billTemplateID}")]
        public Task<BillPaymentStepsTemplateVM> FindBillTemplate(int billTemplateID, CancellationToken cancellationToken = default)
        {
            return TasqR.RunAsync(new FindBillPaymentStepsTemplateQr(billTemplateID), cancellationToken);
        }
    }
}
