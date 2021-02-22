using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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

        public override Task<IEnumerable<BillSummaryVM>> RunAsync(GetBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                return base.RunAsync(process, cancellationToken);

                var request = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.Bill);

                return p_HttpClient.SendAsync(request, cancellationToken)
                    .ContinueWith(responseTask =>
                    {
                        var response = responseTask.Result;

                        if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent)
                        {
                            return Task.FromResult(BillSummaryVM.Empty());
                        }

                        return response.Content.ReadAsStringAsync()
                            .ContinueWith(jsonTask =>
                            {
                                var json = jsonTask.Result;

                                return JsonSerializer.Deserialize<IEnumerable<BillSummaryVM>>(json);
                            });
                    })
                    .Unwrap();
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return Task.FromResult(BillSummaryVM.Empty());
            }
        }
    }
}