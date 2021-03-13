using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.MobileApp.Common.Interfaces
{
    public interface INotificationReceiver
    {
        event EventHandler<string> NotificationReceived;
        event EventHandler<string> ErrorReceived;

        void RaiseNotificationReceived(string message);
        void RaiseErrorReceived(string message);
    }
}
