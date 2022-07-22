using MedikTapp.Resources.Fonts;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel
    {
        public ICommand AboutTappedCmd { get; set; }
        public ICommand AppearanceTappedCmd { get; set; }
        public override bool CanHaveBadge { get; set; } = false;
        public IAsyncCommand ChangePasswordCmd { get; }
        public ICommand HelpTappedCmd { get; set; }
        public override string Icon => FontAwesomeIcons.Gear;
        public bool IsAboutExpanded { get; set; }
        public bool IsAppearanceExpanded { get; set; }
        public bool IsBiometricLoginEnabled { get; set; }
        public ICommand IsBiometricLoginToggledCmd { get; }
        public bool IsDarkModeEnabled { get; set; }
        public ICommand IsDarkModeToggledCmd { get; }
        public bool IsHelpExpanded { get; set; }
        public bool IsNotificationExpanded { get; set; }
        public bool IsPrivacyExpanded { get; set; }
        public bool IsPromoNotificaitonEnabled { get; set; }
        public ICommand IsPromoNotificationToggledCmd { get; }
        public IAsyncCommand LogoutCmd { get; }
        public ICommand NotificationTappedCmd { get; set; }
        public ICommand PrivacyTappedCmd { get; set; }
        public override string Text => "Settings";
        public override View ViewTemplate => new SettingsTab(this);
    }
}