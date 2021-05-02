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
        private readonly ILogger p_ExceptionHandler;
        private readonly ILynxAPI p_HttpClient;

        public GetUserBillsQrHandler_API
            (
                ILynxAPI clientFactory, 
                ILogger<GetUserBillsQrHandler_API> exceptionHandler
            )
        {
            p_HttpClient = clientFactory;
            p_ExceptionHandler = exceptionHandler;
        }

        public async override Task<IEnumerable<UserBillSummaryVM>> RunAsync(GetUserBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpResponse = await p_HttpClient.GetAsync<IEnumerable<UserBillSummaryVM>>
                    (
                        $"{APIUriConstants.UserBill}?forecastDays={process.ForecastDays}", 
                        cancellationToken
                    );
       
                return httpResponse.ObjectContent;
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return UserBillSummaryVM.Empty();
            }
        }
    }
}