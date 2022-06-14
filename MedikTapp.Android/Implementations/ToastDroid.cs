using Android.App;
using Android.Widget;
using MedikTapp.Interfaces;

namespace MedikTapp.Droid.Implementations
{
    public class ToastDroid : IToast
    {
        public void Show(string message) =>
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
    }
}