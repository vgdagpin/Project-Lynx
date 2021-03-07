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
using TasqR;

namespace Lynx.MobileApp.Portable.Handlers.Commands.UtilitiesCmds
{
    public class PingAPICmdHandler_API : TasqHandlerAsync<PingAPICmd, APIPingResult>
    {
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly HttpClient p_HttpClient;

        public PingAPICmdHandler_API
            (
                IHttpClientFactory clientFactory,
                IExceptionHandler exceptionHandler
            )
        {
            p_ExceptionHandler = exceptionHandler;
            p_HttpClient = clientFactory.LynxApiClient();
        }

        public async override Task<APIPingResult> RunAsync(PingAPICmd request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.Ping);
                var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);

                if (httpResponse.IsSuccessStatusCode && httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    return new APIPingResult
                    {
                        IsOnline = true
                    };
                }

                string response = await httpResponse.Content.ReadAsStringAsync();

                return new APIPingResult
                {
                    IsOnline = true,
                    Message = response
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
