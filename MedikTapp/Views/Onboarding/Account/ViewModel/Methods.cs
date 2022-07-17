using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using Newtonsoft.Json;
using Plugin.Fingerprint.Abstractions;
using Refit;
using System;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        private bool AllLoginValidations()
        {
            return !string.IsNullOrWhiteSpace(LoginEmailAddress) && !string.IsNullOrWhiteSpace(LoginPassword);
        }

        private bool AllRegistrationValidatons()
        {
            return AccountPageTemplate != "Login"
                && IsRegisterContactNumberValid
                && IsRegisterEmailAddressValid
                && IsRegisterFirstNameValid
                && IsRegisterLastNameValid
                && IsRegisterPasswordValid
                && !string.IsNullOrWhiteSpace(RegisterAddress)
                && !string.IsNullOrWhiteSpace(RegisterSex)
                && IsRegisterBirthDateValid;
        }

        private async void AppConfigInitialized(object sender, EventArgs e)
        {
            ChangeBiometricsCanExecute(_appConfigService.IsBiometricLoginEnabled
                && await _fingerprint.IsAvailableAsync().ConfigureAwait(false));
        }

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
                    // Login to MedikTapp using biometrics
                    await Login(true);
                else if (result.Status == FingerprintAuthenticationResultStatus.TooManyAttempts)
                    // Retry
                    _toast.Show("Please login using your email and password");
                ChangeBiometricsCanExecute(false);
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

        private Task JoinUs(ScrollView view)
        {
            var secondGrid = ((Grid)view.Content).Children[1];
            return view.ScrollToAsync(secondGrid, ScrollToPosition.Start, true);
        }

        private async Task Login(bool isFromFingerprint)
        {
            if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    if (isFromFingerprint)
                    {
                        LoginEmailAddress = _appConfigService.EmailAddress;
                        LoginPassword = _appConfigService.Password;
                    }

                    var patientCredentials = await _httpService.Login(new PatientModel
                    {
                        EmailAddress = isFromFingerprint ? _appConfigService.EmailAddress : LoginEmailAddress,
                        Password = isFromFingerprint ? _appConfigService.Password : LoginPassword
                    }).ConfigureAwait(false);

                    if (patientCredentials != null)
                        await UpdateLocalPatientConfig(patientCredentials)
                            .ContinueWith(async x => await _mainThread.InvokeOnMainThreadAsync(async () =>
                            {
                                await NavigationService.GoTo<MainPage.MainPage>();
                            }));
                }
                catch (ApiException ex)
                {
                    if (ex.StatusCode == HttpStatusCode.NotFound)
                        // Wrong credentials
                        await _mainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Wrong credentials",
                                "Patient's credentials are not found. Please re-check entered email address or password if it is correct",
                                "OK");
                        });
                    else
                        await _mainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await Application.Current.MainPage.DisplayAlert("Technical error",
                                $"Please contact MedikTapp admin and send this screenshot: {JsonConvert.SerializeObject(ex, Formatting.Indented)}",
                                "OK");
                        });
                }
            }
            else
                await _mainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("No internet",
                        "Sorry, it seems MedikTapp's server is unreachable. Have you tried turning on your mobile data or wi-fi for internet connectivity?",
                        "Ok");
                });
        }

        private void OnLoginCredentialsChanged()
        {
            LoginCmd.RaiseCanExecuteChanged();
        }

        private void OnRegisterCredentialsChanged()
        {
            RegisterCmd.RaiseCanExecuteChanged();
        }

        private async Task Register()
        {
            try
            {
                await _httpService.Register(new PatientModel
                {
                    Age = (DateTime.MinValue + (DateTime.Now - RegisterBirthDate)).Year - 1,
                    Address = RegisterAddress.Trim(),
                    BirthDate = RegisterBirthDate,
                    ContactNumber = RegisterContactNumber.Trim(),
                    EmailAddress = RegisterEmailAddress.Trim(),
                    FirstName = RegisterFirstName.Trim(),
                    LastName = RegisterLastName.Trim(),
                    Password = RegisterPassword,
                    Sex = RegisterSex.Trim()
                }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await _mainThread.InvokeOnMainThreadAsync(async () =>
                {
                    var goToLogin = await Application.Current.MainPage.DisplayAlert("Duplicate record",
                    "Patient is already registered. Would you like to login instead?",
                    "OK",
                    "Cancel");

                    if (goToLogin)
                        AccountPageTemplate = "Login";
                });
            }
        }

        private Task UpdateLocalPatientConfig(PatientModel patientCredentials)
        {
            return Task.WhenAll
            (
                _appConfigService.UpdateConfig("Address", patientCredentials.Address),
                _appConfigService.UpdateConfig("Age", patientCredentials.Age),
                _appConfigService.UpdateConfig("BirthDate", patientCredentials.BirthDate),
                _appConfigService.UpdateConfig("ContactNumber", patientCredentials.ContactNumber),
                _appConfigService.UpdateConfig("EmailAddress", patientCredentials.EmailAddress),
                _appConfigService.UpdateConfig("FirstName", patientCredentials.FirstName),
                _appConfigService.UpdateConfig("LastName", patientCredentials.LastName),
                _appConfigService.UpdateConfig("Password", patientCredentials.Password),
                _appConfigService.UpdateConfig("PatientId", patientCredentials.PatientId),
                _appConfigService.UpdateConfig("Sex", patientCredentials.Sex)
            );
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            AccountPageTemplate = "Register";
            RegisterBirthDate = DateTime.Now;
        }
    }
}