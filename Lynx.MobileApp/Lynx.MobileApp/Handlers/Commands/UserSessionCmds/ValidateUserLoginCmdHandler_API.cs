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
using Lynx.Domain.Models;
using Lynx.Domain.ViewModels;
using Lynx.Interfaces;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Common.Interfaces;
using Lynx.Queries.FirebaseTokenQrs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TasqR;

namespace Lynx.MobileApp.Handlers.Commands.UserLoginCmds
{
    public class ValidateUserLoginCmdHandler_API : TasqHandlerAsync<ValidateUserLoginCmd, LoginResultVM>
    {
        private readonly ILogger p_ExceptionHandler;
        private readonly ITasqR p_TasqR;
        private readonly ILynxAPI p_HttpClient;
        private readonly DbSet<UserSession> p_UserSessionDbSet;
        private readonly DbContext p_BaseDbContext;

        public ValidateUserLoginCmdHandler_API
            (
                ILynxAPI clientFactory,
                ILogger<ValidateUserLoginCmdHandler_API> exceptionHandler,
                ILynxDbContext dbContext,
                ITasqR tasqR
            )
        {
            p_ExceptionHandler = exceptionHandler;
            p_TasqR = tasqR;
            p_HttpClient = clientFactory;
            p_BaseDbContext = dbContext as DbContext;
            p_UserSessionDbSet = (DbSet<UserSession>)dbContext.UserSessions;
        }

        public async override Task<LoginResultVM> RunAsync(ValidateUserLoginCmd process,
            CancellationToken cancellationToken = default)
        {
            try
            {
                string firebaseToken = p_TasqR.Run(new FindMyFirebaseTokenQr());

                var loginRequest = new LoginRequestVM { Username = process.Username, Password = process.Password, FirebaseToken = firebaseToken };

                var httpResponse = await p_HttpClient.PostAsync<UserSessionBO, LoginRequestVM>(APIUriConstants.AccessToken, loginRequest, cancellationToken);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new LynxException(httpResponse.StringContent);
                }

                var dbUserSession = new UserSession
                {
                    UserID = httpResponse.ObjectContent.UserData.ID,
                    Token = httpResponse.ObjectContent.Token
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