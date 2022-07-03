using MedikTapp.Services.NavigationService;
using MedikTapp.Tables;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel
    {
        private Task BiometricLoginToggled(ToggledEventArgs toggled) =>
            _appConfigService.UpdateConfig(nameof(AppConfig.IsBiometricLoginEnabled), toggled.Value);

        private Task DarkModeToggled(ToggledEventArgs toggled) =>
            _appConfigService.UpdateConfig(nameof(AppConfig.IsDarkModeEnabled), toggled.Value);

        private Task PromoNotificationToggled(ToggledEventArgs toggled) =>
            _appConfigService.UpdateConfig(nameof(AppConfig.IsPromoNotifEnabled), toggled.Value);

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            IsBiometricLoginEnabled = _appConfigService.IsBiometricLoginEnabled;
            IsDarkModeEnabled = _appConfigService.IsDarkModeEnabled;
            IsPromoNotificaitonEnabled = _appConfigService.IsPromoNotifEnabled;
        }
    }
}