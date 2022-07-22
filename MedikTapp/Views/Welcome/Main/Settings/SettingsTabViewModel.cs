using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel : TabItemPageViewModelBase
    {
        private readonly AppConfigService _appConfigService;
        private readonly HttpService _httpService;
        private readonly IMainThread _mainThread;
        private readonly IPreferences _preferences;
        private readonly IToast _toast;
        private readonly NotificationService _notificationService;
        private readonly DatabaseService _databaseService;

        public SettingsTabViewModel(NavigationService navigationService,
            DatabaseService databaseService,
            AppConfigService appConfigService,
            HttpService httpService,
            IMainThread mainThread,
            IPreferences preferences,
            NotificationService notificationService,
            IToast toast) : base(navigationService)
        {
            _appConfigService = appConfigService;
            _databaseService = databaseService;
            _httpService = httpService;
            _mainThread = mainThread;
            _preferences = preferences;
            _toast = toast;
            _notificationService = notificationService;

            AboutTappedCmd = new Command(() => IsAboutExpanded = !IsAboutExpanded);
            ChangePasswordCmd = new AsyncSingleCommand(ChangePassword);
            AppearanceTappedCmd = new Command(() => IsAppearanceExpanded = !IsAppearanceExpanded);
            HelpTappedCmd = new Command(() => IsHelpExpanded = !IsHelpExpanded);
            IsBiometricLoginToggledCmd = new AsyncSingleCommand<ToggledEventArgs>(BiometricLoginToggled);
            IsDarkModeToggledCmd = new AsyncSingleCommand<ToggledEventArgs>(DarkModeToggled);
            IsPromoNotificationToggledCmd = new AsyncSingleCommand<ToggledEventArgs>(PromoNotificationToggled);
            LogoutCmd = new AsyncSingleCommand(Logout);
            NotificationTappedCmd = new Command(() => IsNotificationExpanded = !IsNotificationExpanded);
            PrivacyTappedCmd = new Command(() => IsPrivacyExpanded = !IsPrivacyExpanded);
        }
    }
}