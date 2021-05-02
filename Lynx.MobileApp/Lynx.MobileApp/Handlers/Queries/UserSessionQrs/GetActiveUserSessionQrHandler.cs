using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Enums;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Common.Interfaces;
using Lynx.Queries.FirebaseTokenQrs;
using Lynx.Queries.UserQrs;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserSessionQrs
{
    public class GetActiveUserSessionQrHandler : TasqHandlerAsync<GetActiveUserSessionQr, UserSessionVM>
    {
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly ILynxAPI p_LynxAPI;

        public GetActiveUserSessionQrHandler
            (
                ILynxAPI httpClient, 
                ITasqR tasqR, 
                IAppUser appUser
            )
        {
            p_LynxAPI = httpClient;
            p_TasqR = tasqR;
            p_AppUser = appUser;
        }

        public async override Task<UserSessionVM> RunAsync(GetActiveUserSessionQr request, CancellationToken cancellationToken = default)
        {
            string firebaseToken = p_TasqR.Run(new FindMyFirebaseTokenQr());

            var verifResult = await p_LynxAPI.PostAsync<TokenVerificationResult, string>
                (
                    "/AccessToken/VerifyValidity", 
                    firebaseToken, 
                    cancellationToken
                );

            if (verifResult.ObjectContent.TokenStatus != TokenStatus.Active)
            {
                return null;
            }

            var token = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));

            return token;
        }
    }
}