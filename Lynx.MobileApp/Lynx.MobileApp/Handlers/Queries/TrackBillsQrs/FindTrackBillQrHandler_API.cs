using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Queries.TrackBillsQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.TrackBillsQrs;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.TrackBillsQrs
{
    public class FindTrackBillQrHandler_API : FindTrackBillQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly IJsonSerializer p_JsonSerializer;
        private HttpClient p_HttpClient;

        public FindTrackBillQrHandler_API
            (
                IHttpClientFactory clientFactory,
                ILogger<FindTrackBillQrHandler_API> exceptionHandler,
                ITasqR tasqR,
                IAppUser appUser,
                IJsonSerializer jsonSerializer,
                ILynxDbContext dbContext, IMapper mapper
            ) : base(dbContext, mapper)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
            p_JsonSerializer = jsonSerializer;
        }

        public async override Task InitializeAsync(FindTrackBillQr request, CancellationToken cancellationToken)
        {
            var token = await p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID));
            p_HttpClient = p_ClientFactory.LynxApiClient(token);
        }

        public async override Task<TrackBillBO> RunAsync(FindTrackBillQr request, CancellationToken cancellationToken = default)
        {
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{APIUriConstants.TrackBill}/{request.TrackBillID}");
                var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);

                var jsonContent = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new LynxHttpException(httpResponse);
                }

                return p_JsonSerializer.Deserialize<TrackBillBO>(jsonContent);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return TrackBillBO.Null();
            }
        }
    }
}
