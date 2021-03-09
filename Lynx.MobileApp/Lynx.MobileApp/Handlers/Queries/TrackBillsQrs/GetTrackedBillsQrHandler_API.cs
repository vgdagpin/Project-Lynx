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

        public override Task InitializeAsync(GetTrackBillsQr request, CancellationToken cancellationToken)
        {
            return p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID))
                .ContinueWith(result =>
                {
                    p_HttpClient = p_ClientFactory.LynxApiClient(result.Result);
                });
        }

        public async override Task<IEnumerable<TrackBillSummaryVM>> RunAsync(GetTrackBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.TrackBill);

                var response = await p_HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseStream = await response.Content.ReadAsStreamAsync();

                    return await JsonSerializer.DeserializeAsync<List<TrackBillSummaryVM>>(responseStream);
                }
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);
            }

            return await base.RunAsync(process, cancellationToken);
        }
    }
}
