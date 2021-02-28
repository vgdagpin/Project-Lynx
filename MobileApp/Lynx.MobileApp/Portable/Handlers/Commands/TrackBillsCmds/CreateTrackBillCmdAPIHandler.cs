using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Commands.TrackBillsCmds;
using Lynx.Commands.TrackBillCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;

namespace Lynx.MobileApp.Portable.Handlers.Commands.TrackBillsCmds
{
    public class CreateTrackBillCmdAPIHandler : CreateTrackBillCmdHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly HttpClient p_HttpClient;

        public CreateTrackBillCmdAPIHandler(IHttpClientFactory clientFactory, IExceptionHandler exceptionHandler)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_HttpClient = p_ClientFactory.LynxApiClient();
        }

        public override Task<CreateResult<TrackBillVM>> RunAsync(CreateTrackBillCmd process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, APIUriConstants.TrackBill);

                return p_HttpClient.SendAsync(request, cancellationToken)
                    .ContinueWith(responseTask =>
                    {
                        var response = responseTask.Result;

                        if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent)
                        {
                            return Task.FromResult(new CreateResult<TrackBillVM>
                            {
                            });
                        }

                        return response.Content.ReadAsStringAsync()
                            .ContinueWith(jsonTask =>
                            {
                                var json = jsonTask.Result;

                                return JsonSerializer.Deserialize<CreateResult<TrackBillVM>>(json);
                            });
                    })
                    .Unwrap();
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return Task.FromResult(new CreateResult<TrackBillVM>
                {
                });
            }
        }
    }
}
