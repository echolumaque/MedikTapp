using MedikTapp.Constants;
using MedikTapp.Services.NavigationService;
using Plugin.Fingerprint.Abstractions;
using Refit;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        private async Task BiometricsLogin()
        {
            var isFingerPrintAuthAvailable = await _fingerprint.IsAvailableAsync();
            if (isFingerPrintAuthAvailable)
            {
                var result = await _fingerprint.AuthenticateAsync(new("Fingerprint authentication",
                    "Login without entering your email and password!"));
                if (result.Status == FingerprintAuthenticationResultStatus.Succeeded)
                {
                    // login to mediktapp
                    await NavigationService.GoTo<MainPage.MainPage>();
                }
                else if (result.Status == FingerprintAuthenticationResultStatus.TooManyAttempts)
                {
                    // retry
                    _toast.Show("Please login using your email and password");
                    ChangeBiometricsCanExecute(false);
                }
            }
            else
                // No fingerprint available
                ChangeBiometricsCanExecute(false);
        }

        private void ChangeBiometricsCanExecute(bool isBiometricsAvailable)
        {
            IsBiometricsAvailable = isBiometricsAvailable;
            BiometricsCmd.RaiseCanExecuteChanged();
        }

        private void ChangeTemplate(string templateKey)
        {
            _templateKey = templateKey;
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];
        }

        private async Task Continue()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (_templateKey == "Register")
                {
                    try
                    {
                        // successful registration
                        var patientCredentials = await _httpService.Register(new()
                        {
                            { "email", Email },
                            { "password", Password },
                            { "name", Name },
                        });
                    }
                    catch (ApiException)
                    {
                        await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Error", "Patient is already registered on MedikTapp's record", "Ok"));
                    }
                    ChangeTemplate("Login");
                }
                else
                {
                    try
                    {
                        var patientCredentials = await _httpService.Login(new()
                        {
                            { "email", Email },
                            { "password", Password },
                        });

                        if (patientCredentials != null)
                            Device.BeginInvokeOnMainThread(() => NavigationService.SetRootPage<MainPage.MainPage>());
                        else
                            await Application.Current.MainPage.DisplayAlert("Error", "Patient is not yet registered on MedikTapp's record or invalid login credentials.", "Ok");
                    }
                    catch (ApiException)
                    {
                        await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Error", "Patient is not yet registered on MedikTapp's record or invalid login credentials.", "Ok"));
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("No internet",
                    "Sorry, it seems MedikTapp's server is unavailable. Have you tried turning on your mobile data or wi-fi for internet connectivity?",
                    "Ok");
            }
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            _templateKey = "Register";
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];

            ChangeBiometricsCanExecute(_preferences.Get(PreferencesKeys.IsBiometricLoginEnabled, false)
                && await _fingerprint.IsAvailableAsync().ConfigureAwait(false));
        }
    }
}