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
using Lynx.MobileApp.Common.Interfaces;

namespace Lynx.MobileApp.Droid.Common
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingServiceEx : FirebaseMessagingService
    {
        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);

            System.Diagnostics.Debug.WriteLine(p0);
        }

        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);

            string msg = p0.Data["body"];

            var notifReceiver = Xamarin.Forms.DependencyService.Get<INotificationReceiver>();

            notifReceiver.RaiseNotificationReceived(msg);
        }
    }
}