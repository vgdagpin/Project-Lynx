using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Lynx.Application.Handlers.Queries.BillsQrs;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.BillsQrs;

namespace Lynx.MobileApp.Handlers.Queries.BillsQrs
{
    public class GetBillsQrAPIHandler : GetBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly HttpClient p_HttpClient;

        public GetBillsQrAPIHandler(ILynxDbContext dbContext, IMapper mapper, IHttpClientFactory clientFactory, IExceptionHandler exceptionHandler)
            : base(dbContext, mapper)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_HttpClient = p_ClientFactory.LynxApiClient();
        }

        public override IEnumerable<BillSummaryVM> Run(GetBillsQr process)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.Bill);

                var response = p_HttpClient.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = response.Content.ReadAsStreamAsync().Result;

                    return JsonSerializer.DeserializeAsync<List<BillSummaryVM>>(responseStream).Result;
                }
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);
            }

            return base.Run(process);
        }
    }
}
