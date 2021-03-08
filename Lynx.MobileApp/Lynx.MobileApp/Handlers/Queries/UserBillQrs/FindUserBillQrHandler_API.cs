using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Queries.UserBillQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Common.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class FindUserBillQrHandler_API : FindUserBillQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly IJsonSerializer p_JsonSerializer;
        private HttpClient p_HttpClient;

        public FindUserBillQrHandler_API
            (
                IHttpClientFactory clientFactory, 
                IExceptionHandler exceptionHandler, 
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

        public async override Task InitializeAsync(FindUserBillQr request, CancellationToken cancellationToken)
        {
            var token = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));
            p_HttpClient = p_ClientFactory.LynxApiClient(token);
        }

        public async override Task<UserBillVM> RunAsync(FindUserBillQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{APIUriConstants.UserBill}/{process.UserBillID}");
                var httpResponse = await p_HttpClient.SendAsync(request, cancellationToken);

                var jsonContent = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return UserBillVM.Null();
                }

                if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new LynxUnauthorizedException();
                }

                return p_JsonSerializer.Deserialize<UserBillVM>(jsonContent);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return UserBillVM.Null();
            }
        }
    }
}
