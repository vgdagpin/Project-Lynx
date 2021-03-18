using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Application.Handlers.Queries.BillPaymentStepsTemplateQrs;
using Lynx.Commands.AuthenticationCmds;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.Queries.BillPaymentStepsTemplateQrs;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Queries.BillPaymentStepsTemplateQrs
{
    public class GetBillPaymentStepsTemplatesQrHandler_API : GetBillPaymentStepsTemplatesQrHandler
    {
        private readonly IHttpClientFactory p_ClientFactory;
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly IAppUser p_AppUser;
        private readonly IJsonSerializer p_JsonSerializer;
        private HttpClient p_HttpClient;

        public GetBillPaymentStepsTemplatesQrHandler_API
            (
                IHttpClientFactory clientFactory,
                ILogger<GetBillPaymentStepsTemplatesQrHandler_API> exceptionHandler,
                ITasqR tasqR,
                IAppUser appUser,
                IJsonSerializer jsonSerializer,
                ILynxDbContext dbContext
            ) : base(dbContext)
        {
            p_ClientFactory = clientFactory;
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_AppUser = appUser;
            p_JsonSerializer = jsonSerializer;
        }

        public override Task InitializeAsync(GetBillPaymentStepsTemplatesQr request, CancellationToken cancellationToken)
        {
            return p_TasqR.RunAsync(new GetTokenCmd(p_AppUser.UserID))
                .ContinueWith(result =>
                {
                    p_HttpClient = p_ClientFactory.LynxApiClient(result.Result);
                });
        }

        public async override Task<IEnumerable<BillPaymentStepsTemplateSummaryVM>> RunAsync(GetBillPaymentStepsTemplatesQr request, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUri = $"{APIUriConstants.Bill}/{request.BillID}/PaymentStepsTemplate?query={request.Query}";
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri);
                var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);
                var jsonData = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                    {
                        return new List<BillPaymentStepsTemplateSummaryVM>();
                    }

                    return p_JsonSerializer.Deserialize<IEnumerable<BillPaymentStepsTemplateSummaryVM>>(jsonData);
                }

                throw new LynxException(jsonData);
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return new List<BillPaymentStepsTemplateSummaryVM>();
            }
        }
    }
}