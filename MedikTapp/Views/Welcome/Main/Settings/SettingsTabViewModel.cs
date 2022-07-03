using MedikTapp.Helpers.Command;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel : TabItemPageViewModelBase
    {
        private readonly AppConfigService _appConfigService;

        public SettingsTabViewModel(NavigationService navigationService, AppConfigService appConfigService) : base(navigationService)
        {
            _appConfigService = appConfigService;

            AboutTappedCmd = new Command(() => IsAboutExpanded = !IsAboutExpanded);
            AppearanceTappedCmd = new Command(() => IsAppearanceExpanded = !IsAppearanceExpanded);
            HelpTappedCmd = new Command(() => IsHelpExpanded = !IsHelpExpanded);
            IsBiometricLoginToggledCmd = new AsyncSingleCommand<ToggledEventArgs>(BiometricLoginToggled);
            IsDarkModeToggledCmd = new AsyncSingleCommand<ToggledEventArgs>(DarkModeToggled);
            IsPromoNotificationToggledCmd = new AsyncSingleCommand<ToggledEventArgs>(PromoNotificationToggled);
            NotificationTappedCmd = new Command(() => IsNotificationExpanded = !IsNotificationExpanded);
            PrivacyTappedCmd = new Command(() => IsPrivacyExpanded = !IsPrivacyExpanded);
        }
    }
}