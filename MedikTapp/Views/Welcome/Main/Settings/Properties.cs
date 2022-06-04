using MedikTapp.Resources.Fonts;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel
    {
        public override string Icon => FontAwesomeIcons.Gear;
        public override string Text => "Settings";
        public override View ViewTemplate => new SettingsTab(this);
        public override bool CanHaveBadge => false;
    }
}