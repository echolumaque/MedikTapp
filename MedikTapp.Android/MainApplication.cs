using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.FirebasePushNotification;
using System;
using System.Diagnostics;

namespace MedikTapp.Droid
{
    [Application(Theme = "@style/MainTheme")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) { }

        public override void OnCreate()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                FirebasePushNotificationManager.DefaultNotificationChannelId = "Promo Notificaitons";
                FirebasePushNotificationManager.DefaultNotificationChannelName = "Promo Notificaitons";
                FirebasePushNotificationManager.DefaultNotificationChannelImportance = NotificationImportance.Max;
            }

            FirebasePushNotificationManager.Initialize(this, Debugger.IsAttached);
        }
    }
}