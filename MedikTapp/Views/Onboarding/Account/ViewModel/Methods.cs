using MedikTapp.Services.NavigationService;
using MedikTapp.Tables;
using Plugin.Fingerprint.Abstractions;
using Refit;
using System;
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
                    "Login without entering your email and password!")
                {
                    AllowAlternativeAuthentication = true
                });

                if (result.Status == FingerprintAuthenticationResultStatus.Succeeded)
                {
                    // login to mediktapp
                    await Continue(true);
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

        private void OnCredentialsChanged()
        {
            if (_templateKey == "Register")
                CanContinue = !string.IsNullOrWhiteSpace(Name)
                        && !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
            else
                CanContinue = !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

            ContinueCmd.RaiseCanExecuteChanged();
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

        private async Task Continue(bool isFromFingerprint)
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

                        await Task.WhenAll
                        (
                            _appConfigService.UpdateConfig(nameof(AppConfig.PatientName), Name),
                            _appConfigService.UpdateConfig(nameof(AppConfig.PatientId), patientCredentials.PatientId),
                            _appConfigService.UpdateConfig(nameof(AppConfig.Email), Email),
                            _appConfigService.UpdateConfig(nameof(AppConfig.Password), Password)
                        ).ConfigureAwait(false);

                        ChangeBiometricsCanExecute(false);
                    }
                    catch (ApiException)
                    {
                        await Task.WhenAll
                        (
                            _appConfigService.UpdateConfig(nameof(AppConfig.PatientName), string.Empty),
                            _appConfigService.UpdateConfig(nameof(AppConfig.PatientId), -1),
                            _appConfigService.UpdateConfig(nameof(AppConfig.Email), string.Empty),
                            _appConfigService.UpdateConfig(nameof(AppConfig.Password), string.Empty)
                        ).ConfigureAwait(false);

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
                            { "email", isFromFingerprint ? _appConfigService.Email : Email },
                            { "password", isFromFingerprint ? _appConfigService.Password : Password },
                        });

                        if (patientCredentials != null)
                        {
                            if (!isFromFingerprint)
                            {
                                await Task.WhenAll
                                (
                                    _appConfigService.UpdateConfig(nameof(AppConfig.PatientId), patientCredentials.PatientId),
                                    _appConfigService.UpdateConfig(nameof(AppConfig.Email), Email),
                                    _appConfigService.UpdateConfig(nameof(AppConfig.Password), Password)
                                ).ConfigureAwait(false);
                            }
                            else
                                await _appConfigService.UpdateConfig(nameof(AppConfig.PatientId), patientCredentials.PatientId).ConfigureAwait(false);

                            _mainThread.BeginInvokeOnMainThread(() => NavigationService.SetRootPage<MainPage.MainPage>());
                        }
                        else
                        {
                            await Task.WhenAll
                            (
                                _appConfigService.UpdateConfig(nameof(AppConfig.PatientId), -1),
                                _appConfigService.UpdateConfig(nameof(AppConfig.Email), string.Empty),
                                _appConfigService.UpdateConfig(nameof(AppConfig.Password), string.Empty)
                            ).ConfigureAwait(false);

                            await Application.Current.MainPage.DisplayAlert("Error",
                                "Patient is not yet registered on MedikTapp's record or invalid login credentials.", "Ok");
                        }
                    }
                    catch (ApiException)
                    {
                        await _mainThread.InvokeOnMainThreadAsync(async () =>
                            await Application.Current.MainPage.DisplayAlert("Error",
                            "Patient is not yet registered on MedikTapp's record or invalid login credentials.", "Ok"));
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

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _templateKey = "Register";
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];
        }

        private async void AppConfigInitialized(object sender, EventArgs e)
        {
            ChangeBiometricsCanExecute(_appConfigService.IsBiometricLoginEnabled
                && await _fingerprint.IsAvailableAsync().ConfigureAwait(false));
        }
    }
}