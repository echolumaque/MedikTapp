using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel
    {
        private Task GotoServiceConfirmationPopup(Models.Services service)
        {
            return NavigationService.GoTo<ServiceConfirmationPopup>(new()
            {
                { "service", service }
            });
        }

        private void InitServices()
        {
            ServicesCollection = new(_medikTappService.MedikTappServices.Take(6));
            PromosCollection = new(_medikTappService.MedikTappPromos);
            IsLoadingData = false;

            if (!_isTimed)
            {
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    PromoPosition = PromoPosition < PromosCollection.Count - 1
                        ? PromoPosition += 1
                        : PromoPosition = 0;
                    return true;
                });
                _isTimed = true;
            }
        }

        private void OnMedikTappServiceInitialized(object sender, EventArgs e)
        {
            InitServices();
        }

        private async Task Refresh()
        {
            IsRefreshing = true;
            _medikTappService.MedikTappServices = await _httpService.GetServices();
            _medikTappService.MedikTappPromos = await _httpService.GetPromos();
            InitServices();
            IsRefreshing = false;
        }
        public override async void Initialized(NavigationParameters parameters)
        {
            if (_medikTappService.MedikTappServices == null || _medikTappService.MedikTappPromos == null)
            {
                _medikTappService.MedikTappServices = await _httpService.GetServices();
                _medikTappService.MedikTappPromos = await _httpService.GetPromos();
            }

            InitServices();
        }

        private async void OnAppConfigInitialized(object sender, EventArgs e)
        {
            if (_appConfigService.IsBiometricLoginEnabled)
            {
                var isFingerPrintAuthAvailable = await _fingerprint.IsAvailableAsync();
                if (isFingerPrintAuthAvailable)
                {
                    await _mainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        var result = await _fingerprint.AuthenticateAsync(new("Fingerprint authentication",
                        "Login without entering your email and password!")
                        {
                            AllowAlternativeAuthentication = true
                        });

                        if (result.Status == FingerprintAuthenticationResultStatus.Succeeded)
                            IsMainPageVisible = true;
                        else
                        {
                            _toast.Show("Login failed. We are now closing the app.");
                            await Task.Delay(1500);
                            _closeApplication.CloseApplication();
                        }
                    });
                }
                else
                    IsMainPageVisible = true;
            }
            else
                IsMainPageVisible = true;
        }
    }
}