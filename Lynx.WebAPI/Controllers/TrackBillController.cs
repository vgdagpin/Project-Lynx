using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.TrackBillsQrs;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class TrackBillController : LynxBaseController
    {
        public TrackBillController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<TrackBillSummaryVM>> Get()
        {
            return await TasqR.RunAsync(new GetUserTrackedBillsQr(AppUser.UserID));
        }

        [HttpGet("{id}")]
        public async Task<TrackBillVM> Get(Guid id)
        {
            return await TasqR.RunAsync(new GetTrackBillQr(id));
        }
    }
}
