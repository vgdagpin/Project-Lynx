using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Queries.TrackBillsQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.TrackBillsQrs;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.TrackBillsQrs
{
    public class GetTrackedBillsQrHandler_API : GetTrackedBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private HttpClient p_HttpClient;

        public GetTrackedBillsQrHandler_API(IHttpClientFactory clientFactory, ILogger<GetTrackedBillsQrHandler_API> exceptionHandler, ITasqR tasqR, IAppUser appUser)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
        }

        public async override Task InitializeAsync(GetTrackBillsQr request, CancellationToken cancellationToken)
        {
            var session = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));
            p_HttpClient = p_ClientFactory.LynxApiClient(session);
        }

        public async override Task<IEnumerable<TrackBillSummaryVM>> RunAsync(GetTrackBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.TrackBill);

                var httpResponse = await p_HttpClient.SendAsync(httpRequest);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new LynxHttpException(httpResponse);
                }

                var responseStream = await httpResponse.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<List<TrackBillSummaryVM>>(responseStream);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);
            }

            return await base.RunAsync(process, cancellationToken);
        }
    }
}
