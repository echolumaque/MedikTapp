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
            Popup.Init(this);
            CachedImageRenderer.Init(true);
            LoadApplication(Startup.Init(AddPlatformSpecificServices).GetRequiredService<App>());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static void AddPlatformSpecificServices(IServiceCollection services)
        {
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