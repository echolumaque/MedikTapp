using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FFImageLoading.Forms.Platform;
using MedikTapp.DI;
using MedikTapp.Droid.Implementations;
using MedikTapp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Platform.Droid;
using Rg.Plugins.Popup;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using Platform = Xamarin.Essentials.Platform;

namespace MedikTapp.Droid
{
    [Activity(Label = "MedikTapp",
        Icon = "@mipmap/icon",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize |
        ConfigChanges.Orientation |
        ConfigChanges.UiMode |
        ConfigChanges.ScreenLayout |
        ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Forms.Init(this, savedInstanceState);
            FormsMaterial.Init(this, savedInstanceState);
            Platform.Init(this, savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => this);
            Popup.Init(this);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            SetStatusBarColor();

            NotificationChannelInit();
            LoadApplication(Startup.Init(AddPlatformSpecificServices).GetRequiredService<App>());
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        private void NotificationChannelInit()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                NotificationCenter.CreateNotificationChannel(new NotificationChannelRequest
                {
                    Id = "Appointment Notificaitons",
                    Name = "Appointment Notificaitons",
                    Description = "Allows MedikTapp to notifiy you when your appoinment is incoming"
                });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static void AddPlatformSpecificServices(IServiceCollection services)
        {
            services
                .AddSingleton<IFirebasePushNotification, FirebasePushNotificationManager>()
                .AddSingleton<IToast, ToastDroid>()
                .AddSingleton<IFingerprint, FingerprintImplementation>()
                .AddSingleton<INotificationService, NotificationServiceImpl>();
        }

        private void SetStatusBarColor()
        {
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            Window.SetStatusBarColor(Color.ParseColor("#F5F5F5"));
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                Window.InsetsController?.SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.LightStatusBars,
                        (int)WindowInsetsControllerAppearance.LightStatusBars);
            else
#pragma warning disable
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
#pragma warning restore
        }
    }
}