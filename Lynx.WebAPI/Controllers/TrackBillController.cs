using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.TrackBillCmds;
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
        public Task<IEnumerable<TrackBillSummaryVM>> Get()
        {
            return TasqR.RunAsync(new GetUserTrackedBillsQr(AppUser.UserID));
        }

        [HttpGet("{id}")]
        public Task<TrackBillVM> Get(Guid id)
        {
            return TasqR.RunAsync(new GetTrackBillQr(id));
        }

        [HttpPut]
        public Task<CreateResult<TrackBillVM>> Put(TrackBillVM newEntry, CancellationToken cancellationToken = default)
        {
            return TasqR.RunAsync(new CreateTrackBillCmd(newEntry, true), cancellationToken);
        }
    }
}
