using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.EmailCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.WebAPI.Controllers
{

    [Route("[controller]")]
    public class MailController : LynxBaseController
    {
        private readonly ILogger p_Logger;

        public MailController(ITasqR tasqR, IAppUser appUser, ILogger<MailController> logger) : base(tasqR, appUser)
        {
            p_Logger = logger;
        }

        [HttpPost("/Mail/Parse")]
        [AllowAnonymous]
        public Task<long> ParsePost(CancellationToken cancellationToken = default)
        {
            var parameters = new List<MailPart>();

            p_Logger.LogInformation("Header Count: {0}", Request.Headers.Count);
            foreach (var header in Request.Headers)
            {
                parameters.Add(new MailPart
                {
                    PartType = MailPartType.Header,
                    Key = header.Key,
                    Value = new Dictionary<string, object>
                    {
                        { "Value" , header.Value }
                    }
                });
            }


            p_Logger.LogInformation("Form Count: {0}", Request.Form.Count);
            foreach (var formItem in Request.Form)
            {
                parameters.Add(new MailPart
                {
                    PartType = MailPartType.Form,
                    Key = formItem.Key,
                    Value = new Dictionary<string, object>
                    {
                        { "Value" , formItem.Value }
                    }
                });
            }

            p_Logger.LogInformation("Files Count: {0}", Request.Form.Files.Count);
            foreach (IFormFile file in Request.Form.Files)
            {
                if (file.Length == 0)
                    continue;

                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    parameters.Add(new MailPart
                    {
                        PartType = MailPartType.Attachment,
                        Key = file.FileName,
                        Value = new Dictionary<string, object>
                        {
                            { "Content-Type" , file.ContentType },
                            { "Length" , file.Length },
                            { "ContentDisposition" , file.ContentDisposition },
                            { "Content" , fileBytes }
                        }
                    });
                }
            }

            p_Logger.LogInformation("Mail Parse - POST");

            return TasqR.RunAsync(new SaveMailCmd(parameters), cancellationToken);
        }
    }
}