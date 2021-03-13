using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Lynx.MobileApp.Common.Interfaces;
using Android.Gms.Common;
using Xamarin.Forms;
using AndroidX.Core.App;

namespace Lynx.MobileApp.Droid
{
    [Activity(Label = "Lynx", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string ChannelId = "Test123.Channel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            var receiver = DependencyService.Get<INotificationReceiver>();
            receiver.RaiseNotificationReceived("Registering..");

            if (IsPlayServicesAvailable(receiver))
            {
                CreateNotificationChannel();
                receiver.RaiseNotificationReceived("Ready..");
            }

            receiver.NotificationReceived += receiver_NotificationReceived;
        }

        private void receiver_NotificationReceived(object sender, string e)
        {
            var notif = new NotificationCompat.Builder(this, ChannelId)
                .SetContentTitle("Lynx Notif XXY")
                .SetContentText(e)
                .SetPriority((int)NotificationPriority.High)
                .SetSmallIcon(Resource.Drawable.xamarin_logo);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(0, notif.Build());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsPlayServicesAvailable(INotificationReceiver receiver)
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    receiver.RaiseErrorReceived(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                }
                else
                {
                    receiver.RaiseErrorReceived("This device is not supported");
                    Finish();
                }

                return false;
            }
            else
            {
                receiver.RaiseNotificationReceived("Google Play Services is available");
                return true;
            }
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            var channel = new NotificationChannel
                (
                    ChannelId,
                    "Notification blah",
                    NotificationImportance.Default
                );

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}