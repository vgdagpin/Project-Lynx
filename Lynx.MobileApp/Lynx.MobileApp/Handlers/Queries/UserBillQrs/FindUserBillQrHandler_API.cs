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
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Common.Interfaces;
using Lynx.Queries.UserBillQrs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserBillQrs
{
    public class FindUserBillQrHandler_API : FindUserBillQrHandler
    {
        private readonly ILogger p_ExceptionHandler;
        private readonly ILynxAPI p_LynxAPI;

        public FindUserBillQrHandler_API
            (
                ILynxAPI lynxAPI, 
                ILogger<FindUserBillQrHandler_API> exceptionHandler
            )
        {
            p_LynxAPI = lynxAPI;
            p_ExceptionHandler = exceptionHandler;
        }

        public async override Task<UserBillBO> RunAsync(FindUserBillQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await p_LynxAPI.GetAsync<UserBillBO>($"{APIUriConstants.UserBill}/{process.UserBillID}", cancellationToken);

                return response.ObjectContent;
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return UserBillBO.Null();
            }
        }
    }
}
