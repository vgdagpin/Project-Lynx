﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.TrackBillCmds;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.TrackBillsQrs;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class TrackBillsController : LynxBaseController
    {
        public TrackBillsController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet]
        public Task<IEnumerable<TrackBillSummaryBO>> Get()
        {
            return TasqR.RunAsync(new GetTrackBillsQr(AppUser.UserID));
        }

        [HttpGet("{id}")]
        public Task<TrackBillBO> Get(Guid id)
        {
            return TasqR.RunAsync(new FindTrackBillQr(id));
        }

        [HttpPost("Create")]
        public Task<CreateResult<TrackBillBO>> Create(TrackBillBO newEntry, CancellationToken cancellationToken = default)
        {
            return TasqR.RunAsync(new CreateTrackBillCmd(newEntry, true), cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return TasqR.RunAsync(new DeleteTrackBillCmd(id), cancellationToken);
        }
    }
}
