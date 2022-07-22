using Android.Graphics;
using Android.OS;
using Android.Views;
using MedikTapp.Interfaces;
using Xamarin.Essentials;

namespace MedikTapp.Droid.Implementations
{
    public class StatusBarStyleDroid : IStatusBarStyle
    {
        public void SetStatusBarColor(string hexColorString)
        {
            var window = Platform.CurrentActivity.Window;
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(Color.ParseColor(hexColorString));
            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                window.InsetsController?.SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.LightStatusBars,
                        (int)WindowInsetsControllerAppearance.LightStatusBars);
            else
#pragma warning disable
                window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
#pragma warning restore
        }
    }
}