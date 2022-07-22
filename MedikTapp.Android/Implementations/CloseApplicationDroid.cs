using MedikTapp.Interfaces;
using Xamarin.Essentials;

namespace MedikTapp.Droid.Implementations
{
    public class CloseApplicationDroid : ICloseApplication
    {
        public void CloseApplication()
        {
            Platform.CurrentActivity.FinishAndRemoveTask();
        }
    }
}