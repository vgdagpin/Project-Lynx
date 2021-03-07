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
using Lynx.Queries.UserQrs;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.UserSessionQrs
{
    public class GetActiveUserSessionQrHandler : TasqHandlerAsync<GetActiveUserSessionQr, UserSessionVM>
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILynxDbContext p_DbContext;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly IJsonSerializer p_JsonSerializer;
        private HttpClient p_HttpClient;
        private UserSessionVM p_CurrentSession;

        public GetActiveUserSessionQrHandler(IHttpClientFactory clientFactory, ILynxDbContext dbContext, IDateTime dateTime, ITasqR tasqR, IAppUser appUser, IJsonSerializer jsonSerializer)
        {
            p_ClientFactory = clientFactory;
            p_DbContext = dbContext;
            p_TasqR = tasqR;
            p_AppUser = appUser;
            p_JsonSerializer = jsonSerializer;
        }

        public async override Task InitializeAsync(GetActiveUserSessionQr request, CancellationToken cancellationToken)
        {
            p_CurrentSession = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));
            p_HttpClient = p_ClientFactory.LynxApiClient(p_CurrentSession);
        }

        public async override Task<UserSessionVM> RunAsync(GetActiveUserSessionQr request, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Test 123");
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/AccessToken/VerifyValidity");
            var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);

            var jsonContent = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                return null;
            }

            var verifResult = p_JsonSerializer.Deserialize<TokenVerificationResult>(jsonContent);

            if (verifResult.TokenStatus != TokenStatus.Active)
            {
                return null;
            }

            return p_CurrentSession;
        }
    }
}
