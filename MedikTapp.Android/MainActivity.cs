using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using FFImageLoading.Forms.Platform;
using Firebase.Messaging;
using MedikTapp.Constants;
using MedikTapp.DI;
using MedikTapp.Droid.Implementations;
using MedikTapp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Platform.Droid;
using Rg.Plugins.Popup;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using Platform = Xamarin.Essentials.Platform;

namespace MedikTapp.Droid
{
    [Activity(Label = "MedikTapp",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize |
        ConfigChanges.Orientation |
        ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout |
        ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        private const int RequestLocationId = 0;
        private readonly string[] LocationPermissions = new string[2]
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            FormsMaterial.Init(this, savedInstanceState);
            Platform.Init(this, savedInstanceState);
            FormsMaps.Init(this, savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
            Popup.Init(this);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();

            NotificationChannelInit();
            LoadApplication(Startup.Init(AddPlatformSpecificServices).GetRequiredService<App>());
            MessagingCenter.Subscribe<object, bool>(this, Preferences.PushNotificationSubscription, (args, e)
                => FirebaseTopicSubscription(e));
        }

        private void FirebaseTopicSubscription(bool isSubscribed)
        {
            if (isSubscribed)
                FirebaseMessaging.Instance.SubscribeToTopic("promo");
            else
                FirebaseMessaging.Instance.UnsubscribeFromTopic("promo");
        }

        protected override void OnStart()
        {
            base.OnStart();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
                if (CheckSelfPermission(Manifest.Permission.AccessFineLocation) != Permission.Granted)
                    RequestPermissions(LocationPermissions, RequestLocationId);
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        private void NotificationChannelInit()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannelGroup(new NotificationChannelGroup("MedikTapp", "MedikTapp"));

                var localNotifChannel = new NotificationChannel("Appointment Notifications", "Appointment Notifications", NotificationImportance.Max)
                {
                    Name = "Appointment Notifications",
                    Description = "Allows MedikTapp to notify you when your appoinment is incoming",
                    Importance = NotificationImportance.Max,
                    LightColor = Color.ParseColor("#695CD5"),
                    LockscreenVisibility = NotificationVisibility.Public,
                    Group = "MedikTapp"
                };
                localNotifChannel.SetBypassDnd(true);
                localNotifChannel.EnableLights(true);
                localNotifChannel.EnableVibration(true);
                localNotifChannel.SetShowBadge(true);
                notificationManager.CreateNotificationChannel(localNotifChannel);

                var pushNotifChannel = new NotificationChannel("Promo Notifications", "Promo Notifications", NotificationImportance.Max)
                {
                    Name = "Promo Notifications",
                    Description = "Allows MedikTapp to notify you when a new promo started",
                    Importance = NotificationImportance.Max,
                    LightColor = Color.ParseColor("#695CD5"),
                    LockscreenVisibility = NotificationVisibility.Public,
                    Group = "MedikTapp"
                };
                pushNotifChannel.SetBypassDnd(true);
                pushNotifChannel.EnableLights(true);
                pushNotifChannel.EnableVibration(true);
                pushNotifChannel.SetShowBadge(true);
                notificationManager.CreateNotificationChannel(pushNotifChannel);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == RequestLocationId)
            {
                if ((grantResults.Length == 1) && (grantResults[0] == (int)Permission.Granted))
                {
                    // Permissions granted - display a message.
                }
                else
                {
                    // Permissions denied - display a message.
                }
            }
            else
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static void AddPlatformSpecificServices(IServiceCollection services)
        {
            services.AddSingleton<IStatusBarStyle, StatusBarStyleDroid>()
                .AddSingleton<ICloseApplication, CloseApplicationDroid>()
                .AddSingleton<IToast, ToastDroid>()
                .AddSingleton<IFingerprint, FingerprintImplementation>()
                .AddSingleton<INotificationService, NotificationServiceImpl>();
        }
    }
}