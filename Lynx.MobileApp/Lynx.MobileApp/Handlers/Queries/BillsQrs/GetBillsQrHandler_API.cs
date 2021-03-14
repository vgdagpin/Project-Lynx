using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Lynx.Application.Handlers.Queries.BillsQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.BillsQrs;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.BillsQrs
{
    public class GetBillsQrHandler_API : GetBillsQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        
        private HttpClient p_HttpClient;

        public GetBillsQrHandler_API
            (
                IHttpClientFactory clientFactory, 
                ILogger<GetBillsQrHandler_API> exceptionHandler, 
                ITasqR tasqR, 
                IAppUser appUser,
                ILynxDbContext dbContext,
                IMapper mapper
            )
            : base(dbContext, mapper)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
        }

        public override Task InitializeAsync(GetBillsQr request, CancellationToken cancellationToken)
        {
            return p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID))
                .ContinueWith(result =>
                {
                    p_HttpClient = p_ClientFactory.LynxApiClient(result.Result);
                });
        }

        public async override Task<IEnumerable<BillSummaryVM>> RunAsync(GetBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, APIUriConstants.Bill);
                var response = await p_HttpClient.SendAsync(request, cancellationToken);
                var jsonData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return BillSummaryVM.Empty();
                    }

                    return JsonSerializer.Deserialize<IEnumerable<BillSummaryVM>>(jsonData);
                }

                throw new LynxException(jsonData);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return BillSummaryVM.Empty();
            }
        }
    }
}