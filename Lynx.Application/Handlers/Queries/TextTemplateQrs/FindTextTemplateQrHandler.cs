using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Application.Common.Extensions;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.TextTemplateQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.Application.Handlers.Queries.TextTemplateQrs
{
    public class FindTextTemplateQrHandler : TasqHandlerAsync<FindTextTemplateQr, TextTemplateVM>
    {
        private readonly ILynxDbContext p_DbContext;
        private readonly IDateTime p_DateTime;

        public FindTextTemplateQrHandler(ILynxDbContext dbContext, IDateTime dateTime)
        {
            p_DbContext = dbContext;
            p_DateTime = dateTime;
        }

        public async override Task<TextTemplateVM> RunAsync(FindTextTemplateQr request, CancellationToken cancellationToken = default)
        {
            try
            {
                var tt = await p_DbContext.TextTemplates
                    .Where(a => a.Code == request.Code && a.RecordStatus == RecordStatus.Active)
                    .Select(a => new TextTemplateVM
                    {
                        Code = a.Code,
                        Version = a.Version,
                        Content = a.Content,
                        Title = a.Title,
                        UpdatedOn = a.UpdatedOn
                    })
                    .ToListAsync();

                if (tt.Count > 1)
                {
                    return new TextTemplateVM
                    {
                        Content = "More than 1 result found!"
                    };
                }

                if (tt.Count == 0)
                {
                    var newTemplate = new TextTemplate
                    {
                        Code = request.Code,
                        Content = request.Code,
                        Title = request.Code,
                        RecordStatus = RecordStatus.Active,
                        UpdatedOn = p_DateTime.Now,
                        Version = "1.0"
                    };

                    p_DbContext.TextTemplates.Add(newTemplate);
                    await p_DbContext.SaveChangesAsync();

                    return new TextTemplateVM
                    {
                        Code = newTemplate.Code,
                        Content = newTemplate.Content,
                        Title = newTemplate.Title,
                        UpdatedOn = newTemplate.UpdatedOn,
                        Version = newTemplate.Version
                    };
                }

                return tt.Single();
            }
            catch (Exception ex)
            {
                return new TextTemplateVM
                {
                    Content = ex.InnermostException().Message
                };
            }
        }
    }
}