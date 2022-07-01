using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel : TabItemPageViewModelBase
    {
        private readonly IPreferences _preferences;

        public SettingsTabViewModel(NavigationService navigationService,
            IPreferences preferences) : base(navigationService)
        {
            _preferences = preferences;

            AboutTappedCmd = new Command(() => IsAboutExpanded = !IsAboutExpanded);
            AppearanceTappedCmd = new Command(() => IsAppearanceExpanded = !IsAppearanceExpanded);
            HelpTappedCmd = new Command(() => IsHelpExpanded = !IsHelpExpanded);
            IsBiometricLoginToggledCmd = new Command<ToggledEventArgs>(BiometricLoginToggled);
            IsDarkModeToggledCmd = new Command<ToggledEventArgs>(DarkModeToggled);
            IsPromoNotificationToggledCmd = new Command<ToggledEventArgs>(PromoNotificationToggled);
            NotificationTappedCmd = new Command(() => IsNotificationExpanded = !IsNotificationExpanded);
            PrivacyTappedCmd = new Command(() => IsPrivacyExpanded = !IsPrivacyExpanded);
        }
    }
}