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
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class GetUserBillsQrHandler_API : GetUserBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        
        private HttpClient p_HttpClient;

        public GetUserBillsQrHandler_API(IHttpClientFactory clientFactory, IExceptionHandler exceptionHandler, ITasqR tasqR, IAppUser appUser)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
        }

        public override Task InitializeAsync(GetUserBillsQr request, CancellationToken cancellationToken)
        {
            return p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID))
                .ContinueWith(result =>
                {
                    p_HttpClient = p_ClientFactory.LynxApiClient(result.Result);
                });
        }

        public override Task<IEnumerable<UserBillSummaryVM>> RunAsync(GetUserBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.UserBill);

                return p_HttpClient.SendAsync(request, cancellationToken)
                    .ContinueWith(responseTask =>
                    {
                        var response = responseTask.Result;

                        if (response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.NoContent)
                        {
                            return Task.FromResult(UserBillSummaryVM.Empty());
                        }

                        return response.Content.ReadAsStringAsync()
                            .ContinueWith(jsonTask =>
                            {
                                var json = jsonTask.Result;

                                return JsonSerializer.Deserialize<IEnumerable<UserBillSummaryVM>>(json);
                            });
                    })
                    .Unwrap();
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return Task.FromResult(UserBillSummaryVM.Empty());
            }
        }
    }
}