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
using Lynx.MobileApp.Common.Interfaces;
using Lynx.MobileApp.Droid.Common;
using Xamarin.Forms;

namespace Lynx.MobileApp.Droid.Common
{
    public class NotificationReceiver : INotificationReceiver
    {
        public event EventHandler<string> NotificationReceived;
        public event EventHandler<string> ErrorReceived;

        public void RaiseErrorReceived(string message)
        {
            ErrorReceived?.Invoke(this, message);
        }

        public void RaiseNotificationReceived(string message)
        {
            NotificationReceived?.Invoke(this, message);
        }
    }
}