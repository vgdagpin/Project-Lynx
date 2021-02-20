using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Application.Handlers.Queries.UserBillQrs;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class GetUserBillsQrAPIHandler : GetUserBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly HttpClient p_HttpClient;

        public GetUserBillsQrAPIHandler(ILynxDbContext dbContext, IMapper mapper, IHttpClientFactory clientFactory, IExceptionHandler exceptionHandler)
            : base(dbContext, mapper)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_HttpClient = p_ClientFactory.LynxApiClient();
        }

        public async override Task<IEnumerable<UserBillVM>> RunAsync(GetUserBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "UserBill");

                var response = await p_HttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<List<UserBillVM>>(responseStream);
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