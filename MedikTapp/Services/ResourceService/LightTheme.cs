using MedikTapp.Constants;
using Xamarin.Forms;

namespace MedikTapp.Services.ResourceService
{
    public class LightTheme : ResourceDictionary
    {
        public LightTheme()
        {
            Add(nameof(Colors.LightPurple), Color.FromHex(""));
            Add(nameof(Colors.Purple), Color.FromHex("#6B79E4"));
            Add(nameof(Colors.DarkPurple), Color.FromHex("#695CD5"));
            Add(nameof(Colors.LightGray), Color.FromHex("#F4F5F9"));
            Add(nameof(Colors.Black), Color.FromHex("#2F2F32"));
        }
    }
}