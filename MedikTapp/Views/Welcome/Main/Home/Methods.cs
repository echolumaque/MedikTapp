using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
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

        private void InitServices(bool shouldCreateTimer = false)
        {
            ServicesCollection = new(_medikTappService.MedikTappServices.Take(6));
            PromosCollection = new(_medikTappService.MedikTappPromos);
            IsLoadingData = false;
            if (shouldCreateTimer)
                return;

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                PromoPosition = PromoPosition == PromosCollection.Count - 1
                    ? PromoPosition = 0
                    : PromoPosition += 1;
                return true;
            });
        }

        private void OnMedikTappServiceInitialized(object sender, EventArgs e)
        {
            InitServices(false);
        }

        private async Task Refresh()
        {
            IsRefreshing = true;
            _medikTappService.MedikTappServices = await _httpService.GetServices();
            _medikTappService.MedikTappPromos = await _httpService.GetPromos();
            InitServices(true);
            IsRefreshing = false;
        }
        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (_medikTappService.MedikTappServices == null || _medikTappService.MedikTappPromos == null)
            {
                _medikTappService.MedikTappServices = await _httpService.GetServices();
                _medikTappService.MedikTappPromos = await _httpService.GetPromos();
            }

            InitServices(false);
        }
    }
}