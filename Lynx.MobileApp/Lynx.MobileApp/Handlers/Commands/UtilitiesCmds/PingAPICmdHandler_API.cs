using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Commands.UtilitiesCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Common.Interfaces;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Portable.Handlers.Commands.UtilitiesCmds
{
    public class PingAPICmdHandler_API : TasqHandlerAsync<PingAPICmd, APIPingResult>
    {
        private readonly ILogger p_ExceptionHandler;
        private readonly ILynxAPI p_HttpClient;

        public PingAPICmdHandler_API
            (
                ILynxAPI clientFactory,
                ILogger<PingAPICmdHandler_API> exceptionHandler
            )
        {
            p_ExceptionHandler = exceptionHandler;
            p_HttpClient = clientFactory;
        }

        public async override Task<APIPingResult> RunAsync(PingAPICmd request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpResponse = await p_HttpClient.GetAsync<string>(APIUriConstants.Ping, cancellationToken);

                if (!string.IsNullOrWhiteSpace(httpResponse.StringContent))
                {
                    return new APIPingResult
                    {
                        IsOnline = true,
                        Message = httpResponse.StringContent
                    };
                }

                return new APIPingResult
                {
                    IsOnline = true
                };
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return new APIPingResult
                {
                    IsOnline = false,
                    Message = ex.Message
                };
            }
        }
    }
}
