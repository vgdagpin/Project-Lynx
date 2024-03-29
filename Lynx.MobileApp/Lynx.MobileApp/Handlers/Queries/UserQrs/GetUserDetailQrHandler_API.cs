﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Queries.UserQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Models;
using Lynx.Interfaces;
using Lynx.Queries.UserQrs;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Portable.Handlers.Queries.UserQrs
{
    public class GetUserDetailQrHandler_API : GetUserDetailQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;

        private HttpClient p_HttpClient;

        public GetUserDetailQrHandler_API(IHttpClientFactory clientFactory, ILogger<GetUserDetailQrHandler_API> exceptionHandler, ITasqR tasqR, IAppUser appUser)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
        }

        public override Task InitializeAsync(GetUserDetailQr request, CancellationToken cancellationToken)
        {
            return p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID))
                .ContinueWith(result =>
                {
                    p_HttpClient = p_ClientFactory.LynxApiClient(result.Result);
                });
        }

        public override Task<UserBO> RunAsync(GetUserDetailQr process, CancellationToken cancellationToken = default)
        {
            return base.RunAsync(process, cancellationToken);
        }
    }
}
