using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Queries.TrackBillsQrs;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.TrackBillsQrs;

namespace Lynx.MobileApp.Handlers.Queries.TrackBillsQrs
{
    public class GetUserTrackedBillsQrAPIHandler : GetUserTrackedBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly HttpClient p_HttpClient;

        public GetUserTrackedBillsQrAPIHandler(ILynxDbContext dbContext, IMapper mapper, IHttpClientFactory clientFactory, IExceptionHandler exceptionHandler)
            : base(dbContext, mapper)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_HttpClient = p_ClientFactory.LynxApiClient();
        }

        public async override Task<IEnumerable<TrackBillSummaryVM>> RunAsync(GetUserTrackedBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.TrackBill);

                var response = await p_HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();

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
