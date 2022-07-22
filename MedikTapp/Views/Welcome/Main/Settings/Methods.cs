using MedikTapp.Services.NavigationService;
using MedikTapp.Tables;
using MedikTapp.Views.Onboarding.Account;
using System.Threading.Tasks;
using Xamarin.Forms;
using Preferences = MedikTapp.Constants.Preferences;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTabViewModel
    {
        private Task BiometricLoginToggled(ToggledEventArgs toggled)
        {
            return _appConfigService.UpdateConfig(nameof(AppConfig.IsBiometricLoginEnabled), toggled.Value);
        }

        private async Task ChangePassword()
        {
            var oldPassword = await Application.Current.MainPage.DisplayPromptAsync("Change password", "What is your old password?");
            if (oldPassword == _appConfigService.Password)
            {
                var newPassword = await Application.Current.MainPage.DisplayPromptAsync("Change password", "What is your new password?");
                await _httpService.ChangePassword(_appConfigService.PatientId, newPassword);
                await _appConfigService.UpdateConfig("Password", newPassword)
                    .ContinueWith(_ =>
                    {
                        _mainThread.BeginInvokeOnMainThread(() => _toast.Show("You have succesfully changed your password"));
                    });
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Wrong password", "It seems you entered a wrong password. We can't proceed to change your password right now.", "Ok");
            }
        }
        private Task DarkModeToggled(ToggledEventArgs toggled)
        {
            return _appConfigService.UpdateConfig(nameof(AppConfig.IsDarkModeEnabled), toggled.Value);
        }

        private Task Logout()
        {
            return Application.Current.MainPage.DisplayAlert("Logout", "Are sure you want to logout of MedikTapp?", "Yes", "No")
                .ContinueWith(async confirmation =>
                {
                    // Logout is confirmed
                    if (await confirmation)
                    {
                        await Task.WhenAll
                        (
                            _appConfigService.UpdateConfig("Address", string.Empty),
                            _appConfigService.UpdateConfig("Age", 0),
                            _appConfigService.UpdateConfig("BirthDate", default),
                            _appConfigService.UpdateConfig("ContactNumber", string.Empty),
                            _appConfigService.UpdateConfig("Username", string.Empty),
                            _appConfigService.UpdateConfig("FirstName", string.Empty),
                            _appConfigService.UpdateConfig("LastName", string.Empty),
                            _appConfigService.UpdateConfig("Password", string.Empty),
                            _appConfigService.UpdateConfig("PatientId", 0),
                            _appConfigService.UpdateConfig("Sex", "Male"),
                            _appConfigService.UpdateConfig("IsBiometricLoginEnabled", false),
                            _appConfigService.UpdateConfig("IsDarkModeEnabled", false),
                            _appConfigService.UpdateConfig("IsPromoNotifEnabled", false),
                            _databaseService.DeleteAll<Models.Services>()
                        ).ContinueWith(_ =>
                        {
                            _notificationService.CancelAll();
                            _mainThread.BeginInvokeOnMainThread(() =>
                            {
                                NavigationService.SetRootPage<AccountPage>(isOffWhitePageBgColor: true);
                                _toast.Show("You have succesfully logged out of MedikTapp");
                            });
                            _preferences.Set(Preferences.HasLoggedAccount, false);
                        });
                    }
                });
        }

        private Task PromoNotificationToggled(ToggledEventArgs toggled)
        {
            _notificationService.PromoNotificationsSubscription(toggled.Value);
            return _appConfigService.UpdateConfig(nameof(AppConfig.IsPromoNotifEnabled), toggled.Value);
        }

        public override void Initialized(NavigationParameters parameters)
        {
            IsBiometricLoginEnabled = _appConfigService.IsBiometricLoginEnabled;
            IsDarkModeEnabled = _appConfigService.IsDarkModeEnabled;
            IsPromoNotificaitonEnabled = _appConfigService.IsPromoNotifEnabled;
        }
    }
}