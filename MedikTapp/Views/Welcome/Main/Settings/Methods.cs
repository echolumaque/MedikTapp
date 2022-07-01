using MedikTapp.Constants;
using MedikTapp.Services.NavigationService;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel
    {

        private void BiometricLoginToggled(ToggledEventArgs toggled)
        {
            _preferences.Set(PreferencesKeys.IsBiometricLoginEnabled, toggled.Value);
        }

        private void DarkModeToggled(ToggledEventArgs toggled)
        {
            _preferences.Set(PreferencesKeys.IsDarkModeEnabled, toggled.Value);
        }

        private void PromoNotificationToggled(ToggledEventArgs toggled)
        {
            _preferences.Set(PreferencesKeys.IsPromoNotifEnabled, toggled.Value);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            IsBiometricLoginEnabled = _preferences.Get(PreferencesKeys.IsBiometricLoginEnabled, false);
            IsDarkModeEnabled = _preferences.Get(PreferencesKeys.IsDarkModeEnabled, false);
            IsPromoNotificaitonEnabled = _preferences.Get(PreferencesKeys.IsPromoNotifEnabled, false);
        }
    }
}