using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.TextTemplateQrs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TasqR;

namespace Lynx.WebAPI.Controllers
{
    [Route("[controller]")]
    public class TextTemplateController : LynxBaseController
    {
        public TextTemplateController(ITasqR tasqR, IAppUser appUser) : base(tasqR, appUser)
        {
        }

        [HttpGet("/TextTemplate/{code}")]
        public Task<TextTemplateVM> Get(string code, CancellationToken cancellationToken = default)
        {
            return TasqR.RunAsync(new FindTextTemplateQr(code), cancellationToken);
        }
    }
}
