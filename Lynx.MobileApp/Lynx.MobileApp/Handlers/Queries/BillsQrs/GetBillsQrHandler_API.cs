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
        private readonly ILynxAPI p_LynxAPI;
        private readonly ILogger p_ExceptionHandler;


        public GetBillsQrHandler_API
            (
                ILynxAPI lynxAPI,
                ILogger<GetBillsQrHandler_API> exceptionHandler,
                ILynxDbContext dbContext,
                IMapper mapper
            )
            : base(dbContext, mapper)
        {
            p_LynxAPI = lynxAPI;
            p_ExceptionHandler = exceptionHandler;
        }

        public async override Task<IEnumerable<BillSummaryVM>> RunAsync(GetBillsQr process, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await p_LynxAPI.GetAsync<IEnumerable<BillSummaryVM>>(APIUriConstants.Bill, cancellationToken);

                return response.ObjectContent;
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return BillSummaryVM.Empty();
            }
        }
    }
}