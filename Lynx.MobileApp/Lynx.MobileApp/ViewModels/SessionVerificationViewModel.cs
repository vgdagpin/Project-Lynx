using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Lynx.Commands.UtilitiesCmds;
using Lynx.Domain.ViewModels;
using Lynx.MobileApp.Common.Constants;
using Lynx.MobileApp.Portable.Common.Enums;
using Lynx.MobileApp.Views;
using Lynx.Queries.UserSessionQrs;
using Microsoft.EntityFrameworkCore;
using TasqR;
using Xamarin.Forms;

namespace Lynx.MobileApp.ViewModels
{
    public class SessionVerificationViewModel : BaseViewModel
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

            Task.Run(() =>
            {
                try
                {
                    PreloadProgress += "\nPinging API..";

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

                        OnVerificationCompleteEvent?.Invoke(SessionVerificationResult.NeedLogin, this);
                    }
                    else
                    {
                        PreloadProgress += "\nVerifying authentication token..";

                        UserSessionVM activeSession = TasqR.Run(new GetActiveUserSessionQr());

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
