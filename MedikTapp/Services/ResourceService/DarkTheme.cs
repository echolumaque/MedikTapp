using MedikTapp.Constants;
using Xamarin.Forms;

namespace MedikTapp.Services.ResourceService
{
    public class DarkTheme : ResourceDictionary
    {
        public DarkTheme()
        {
            Add(nameof(Colors.LightPurple), Color.FromHex(""));
            Add(nameof(Colors.Purple), Color.FromHex(""));
            Add(nameof(Colors.DarkPurple), Color.FromHex(""));
        }
    }
}