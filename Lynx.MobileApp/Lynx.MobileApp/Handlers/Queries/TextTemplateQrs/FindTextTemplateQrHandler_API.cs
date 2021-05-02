using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Application.Handlers.Queries.TextTemplateQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.TextTemplateQrs;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.TextTemplateQrs
{
    public class FindTextTemplateQrHandler_API : FindTextTemplateQrHandler
    {
        private readonly ILynxAPI p_LynxAPI;
        private readonly ILogger p_ExceptionHandler;

        public FindTextTemplateQrHandler_API
            (
                ILynxAPI lynxAPI,
                ILogger<FindTextTemplateQrHandler_API> exceptionHandler,
                ILynxDbContext dbContext,
                IDateTime dateTime
            )
            : base(dbContext, dateTime)
        {
            p_LynxAPI = lynxAPI;
            p_ExceptionHandler = exceptionHandler;
        }

        public async override Task<TextTemplateVM> RunAsync(FindTextTemplateQr request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpResponse = await p_LynxAPI.GetAsync<TextTemplateVM>
                    (
                        $"{APIUriConstants.TextTemplate}/{request.Code}", 
                        cancellationToken
                    );

                return httpResponse.ObjectContent;
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return new TextTemplateVM
                {
                    Content = ex.Message
                };
            }
        }
    }
}
