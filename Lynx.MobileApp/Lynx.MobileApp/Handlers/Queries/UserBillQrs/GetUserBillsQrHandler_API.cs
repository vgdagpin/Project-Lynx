using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Lynx.Application.Handlers.Queries.UserBillQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class GetUserBillsQrHandler_API : GetUserBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly IJsonSerializer p_JsonSerializer;
        private HttpClient p_HttpClient;

        public GetUserBillsQrHandler_API
            (
                IHttpClientFactory clientFactory, 
                ILogger<GetUserBillsQrHandler_API> exceptionHandler, 
                ITasqR tasqR, 
                IAppUser appUser,
                IJsonSerializer jsonSerializer
            )
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
            p_JsonSerializer = jsonSerializer;
        }

        public async override Task InitializeAsync(GetUserBillsQr request, CancellationToken cancellationToken)
        {
            var token = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));
            p_HttpClient = p_ClientFactory.LynxApiClient(token);
        }

        public async override Task<IEnumerable<UserBillSummaryVM>> RunAsync(GetUserBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{APIUriConstants.UserBill}?forecastDays={process.ForecastDays}");
                var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);

                string jsonContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new LynxUnauthorizedException();
                }

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return UserBillSummaryVM.Empty();
                }

                return p_JsonSerializer.Deserialize<IEnumerable<UserBillSummaryVM>>(jsonContent);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return UserBillSummaryVM.Empty();
            }
        }
    }
}