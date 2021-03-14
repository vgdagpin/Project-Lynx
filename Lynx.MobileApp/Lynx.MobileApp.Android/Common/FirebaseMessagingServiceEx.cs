using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Lynx.Commands.FirebaseTokenCmds;
using Lynx.MobileApp.Common;
using Lynx.MobileApp.Common.Interfaces;
using Lynx.MobileApp.Handlers.Commands.FirebaseTokenCmds;
using TasqR;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lynx.MobileApp.Droid.Common
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingServiceEx : FirebaseMessagingService
    {
        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            LynxDependencyService.Get<FirebaseTokenManager>().OnNewToken(token);
        }

        public override void OnMessageReceived(RemoteMessage messageData)
        {
            base.OnMessageReceived(messageData);

            string msg = messageData.Data["body"];

            var tasqR = LynxDependencyService.Get<ITasqR>();

            tasqR.UsingAsHandler(typeof(SaveFirebaseTokenCmdHandler_Local))
                .Run(new SaveFirebaseTokenCmd(msg));

            var notifReceiver = LynxDependencyService.Get<INotificationReceiver>();

            notifReceiver.RaiseNotificationReceived(msg);
        }
    }
}