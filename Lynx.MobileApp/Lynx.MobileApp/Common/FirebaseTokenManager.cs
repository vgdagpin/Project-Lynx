using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Commands.FirebaseTokenCmds;
using TasqR;

using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Common
{
    public class FirebaseTokenManager
    {
        public void OnNewToken(string token)
        {
            var tasqR = App.ServiceProvider.GetService<ITasqR>();

            tasqR.Run(new SaveFirebaseTokenCmd(token));
        }
    }
}
