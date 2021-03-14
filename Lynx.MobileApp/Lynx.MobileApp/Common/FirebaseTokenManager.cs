using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Commands.FirebaseTokenCmds;
using Lynx.MobileApp.Handlers.Commands.FirebaseTokenCmds;
using TasqR;

using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Common
{
    public class FirebaseTokenManager
    {
        private string token = null;

        public void OnNewToken(string token)
        {
            this.token = token;
        }

        public void SaveToken()
        {
            var tasqR = LynxDependencyService.Get<ITasqR>();

            tasqR.UsingAsHandler(typeof(SaveFirebaseTokenCmdHandler_Local))
                .Run(new SaveFirebaseTokenCmd(token));
        }
    }
}
