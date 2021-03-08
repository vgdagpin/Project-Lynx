using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.EmailCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.Application.Handlers.Commands.EmailCmds
{
    public class SaveMailCmdHandler : TasqHandlerAsync<SaveMailCmd, long>
    {
        private readonly DbSet<Email> p_EmailDbSet;
        private readonly DbContext p_BaseDbContext;
        private readonly ILogger p_Logger;
        private readonly IDateTime p_DateTime;

        public SaveMailCmdHandler
            (
                ILynxDbContext dbContext, 
                ILogger<SaveMailCmdHandler> logger,
                IDateTime dateTime
            )
        {
            p_EmailDbSet = (DbSet<Email>)dbContext.Emails;
            p_BaseDbContext = (DbContext)dbContext;
            p_Logger = logger;
            p_DateTime = dateTime;
        }

        public async override Task<long> RunAsync(SaveMailCmd request, CancellationToken cancellationToken = default)
        {
            try
            {
                p_Logger.LogInformation("Data count: {0}", request.Data.Count());

                Email email = new Email
                {
                    From = GetFrom(request.Data),
                    To = GetTo(request.Data),
                    CC = GetCC(request.Data),
                    Subject = GetSubject(request.Data),
                    CreatedOn = p_DateTime.Now,
                    N_Body = new EmailBody
                    {
                        Content = GetBody(request.Data)
                    }
                };

                foreach (var item in request.Data.Where(a => a.PartType == MailPartType.Header))
                {
                    email.N_Headers.Add(new EmailHeader
                    {
                        Name = item.Key,
                        Value = item.Value["Value"]?.ToString()
                    });
                }

                foreach (var item in request.Data.Where(a => a.PartType == MailPartType.Attachment))
                {
                    email.N_Attachments.Add(new EmailAttachment
                    {
                        FileName = item.Key,
                        Length = (long)item.Value["Length"],
                        ContentType = (string)item.Value["Content-Type"],
                        Content = (byte[])item.Value["Content"]
                    });
                }

                p_EmailDbSet.Add(email);

                await p_BaseDbContext.SaveChangesAsync();

                return email.ID;
            }
            catch (Exception ex)
            {
                p_Logger.LogError(ex, ex.Message);

                return -1;
            }
        }

        protected virtual string GetBody(IEnumerable<MailPart> data)
        {
            var value = data.FirstOrDefault(a => a.PartType == MailPartType.Form && a.Key == "email")
                ?.Value["Value"];

            return value?.ToString();
        }

        protected virtual string GetTo(IEnumerable<MailPart> data)
        {
            var value = data.FirstOrDefault(a => a.PartType == MailPartType.Form && a.Key == "to")
                ?.Value["Value"];

            return value?.ToString();
        }

        protected virtual string GetSubject(IEnumerable<MailPart> data)
        {
            var value = data.FirstOrDefault(a => a.PartType == MailPartType.Form && a.Key == "subject")
                ?.Value["Value"];

            return value?.ToString();
        }

        protected virtual string GetFrom(IEnumerable<MailPart> data)
        {
            var value = data.FirstOrDefault(a => a.PartType == MailPartType.Form && a.Key == "from")
                ?.Value["Value"];

            return value?.ToString();
        }

        protected virtual string GetCC(IEnumerable<MailPart> data)
        {
            var value = data.FirstOrDefault(a => a.PartType == MailPartType.Form && a.Key == "cc")
                ?.Value["Value"];

            return value?.ToString();
        }
    }
}
