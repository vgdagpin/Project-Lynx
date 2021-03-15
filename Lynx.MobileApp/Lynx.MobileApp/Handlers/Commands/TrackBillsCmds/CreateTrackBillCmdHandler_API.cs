using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Commands.TrackBillsCmds;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Commands.TrackBillCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Portable.Handlers.Commands.TrackBillsCmds
{
    public class CreateTrackBillCmdHandler_API : CreateTrackBillCmdHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly IJsonSerializer p_JsonSerializer;
        private HttpClient p_HttpClient;

        public CreateTrackBillCmdHandler_API
            (
                IHttpClientFactory clientFactory,
                ILogger<CreateTrackBillCmdHandler_API> exceptionHandler,
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

        public override Task InitializeAsync(CreateTrackBillCmd tasq, CancellationToken cancellationToken)
        {
            return p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID))
                .ContinueWith(result =>
                {
                    p_HttpClient = p_ClientFactory.LynxApiClient(result.Result);
                });
        }

        public async override Task<CreateResult<TrackBillVM>> RunAsync(CreateTrackBillCmd process, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{APIUriConstants.TrackBill}/Create")
                {
                    Content = new StringContent(p_JsonSerializer.Serialize(process.Entry), Encoding.UTF8, "application/json")
                };

                var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);
                string json = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new LynxHttpException(httpResponse);
                }

                return JsonSerializer.Deserialize<CreateResult<TrackBillVM>>(json);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex, ex.Message);

                return new CreateResult<TrackBillVM>
                {
                    IsCreated = false,
                    Error = ex
                };
            }
        }
    }
}