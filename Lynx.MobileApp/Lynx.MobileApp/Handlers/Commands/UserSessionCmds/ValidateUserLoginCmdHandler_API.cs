using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Commands.UserLoginCmds;
using Lynx.Common.ViewModels;
using Lynx.Domain.Entities;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Microsoft.EntityFrameworkCore;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserLoginCmds
{
    public class ValidateUserLoginCmdHandler_API : TasqHandlerAsync<ValidateUserLoginCmd, LoginResultVM>
    {
        private readonly IExceptionHandler p_ExceptionHandler;
        private readonly ILynxDbContext p_DbContext;
        private readonly IJsonSerializer p_JsonSerializer;
        private readonly HttpClient p_HttpClient;
        private readonly DbSet<UserSession> p_UserSessionDbSet;
        private readonly DbContext p_BaseDbContext;

        public ValidateUserLoginCmdHandler_API
            (
                IHttpClientFactory clientFactory,
                IExceptionHandler exceptionHandler,
                ILynxDbContext dbContext,
                IJsonSerializer jsonSerializer
            )
        {
            p_ExceptionHandler = exceptionHandler;
            p_DbContext = dbContext;
            p_JsonSerializer = jsonSerializer;
            p_HttpClient = clientFactory.LynxApiClient();
            p_BaseDbContext = dbContext as DbContext;
            p_UserSessionDbSet = (DbSet<UserSession>)dbContext.UserSessions;
        }

        public async override Task<LoginResultVM> RunAsync(ValidateUserLoginCmd process, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var loginRequest = new LoginRequestVM { Username = process.Username, Password = process.Password };
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, APIUriConstants.AccessToken)
                {
                    Content = new JsonContent<LoginRequestVM>(loginRequest)
                };

                var httpResponse = await p_HttpClient.SendAsync(httpRequest, cancellationToken);
                string response = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new LynxException(response);
                }

                var userSession = p_JsonSerializer.Deserialize<UserSessionVM>(response);
                var dbUserSession = new UserSession
                {
                    UserID = userSession.UserData.ID,
                    Token = userSession.Token
                };

                p_UserSessionDbSet.Add(dbUserSession);
                p_BaseDbContext.SaveChanges();

                return new LoginResultVM
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                p_ExceptionHandler.LogError(ex);

                return new LoginResultVM
                {
                    IsSuccess = false,
                    Error = ex.InnermostException()
                };
            }
        }
    }
}
