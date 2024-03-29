﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Commands.UtilitiesCmds;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Common;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Handlers.Queries.UserSessionQrs;
using Lynx.MobileApp.Portable.Common.Enums;
using Lynx.MobileApp.Views;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class SessionVerificationViewModel : LynxViewModel
    {
        #region PreloadProgress
        private string preloadProgress;
        public string PreloadProgress
        {
            get => preloadProgress;
            set => SetProperty(ref preloadProgress, value);
        }
        #endregion

        public delegate void VerificationCompleteEvent(SessionVerificationResult result, object sender);
        public event VerificationCompleteEvent OnVerificationCompleteEvent;

        public SessionVerificationViewModel()
        {
        }

        public void Initialize()
        {
            PreloadProgress = "Loading..";

            Task.Run(async () =>
            {
                try
                {
                    PreloadProgress += "\nPinging API: " + App.Configuration["Lynx-API-Hostname"];

                    var ping = TasqR.Run(new PingAPICmd());

                    if (ping.IsOnline == true)
                    {
                        PreloadProgress += "\nAPI is online";
                    }
                    else
                    {
                        PreloadProgress += "\nAPI is offline";
                        PreloadProgress += $"\nMessage: {ping.Message}";
                    }

                    if (!File.Exists(SQLiteConstants.FilePath))
                    {
                        PreloadProgress += "\nCreating Database..";
                        GetService<DbContext>().Database.Migrate();
                        PreloadProgress += "\nDatabase Created";

                        LynxDependencyService.Get<FirebaseTokenManager>().SaveToken();

                        OnVerificationCompleteEvent?.Invoke(SessionVerificationResult.NeedLogin, this);
                    }
                    else
                    {
                        PreloadProgress += "\nVerifying authentication token..";

                        var activeSession = await TasqR
                            .UsingAsHandler<GetActiveUserSessionQrHandler>()
                            .RunAsync(new GetActiveUserSessionQr());

                        if (activeSession == null)
                        {
                            OnVerificationCompleteEvent?.Invoke(SessionVerificationResult.NeedLogin, this);
                        }
                        else
                        {
                            OnVerificationCompleteEvent?.Invoke(SessionVerificationResult.Authenticated, this);
                        }
                    }
                }
                catch (Exception ex)
                {
                    var innerEx = ex.InnermostException();
                    PreloadProgress = $"Initialize Error: {innerEx.Message}";
                }
            });
        }
    }
}
