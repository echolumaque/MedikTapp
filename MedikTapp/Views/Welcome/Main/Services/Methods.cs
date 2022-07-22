using MedikTapp.Services.NavigationService;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTabViewModel
    {
        private void FilterServices(TextChangedEventArgs args)
        {
            var searchTerm = args.NewTextValue.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(searchTerm))
                searchTerm = string.Empty;

            var filteredItems = _medikTappService.MedikTappServices.Where(x => x.ServiceName.ToLowerInvariant().Contains(searchTerm)).ToList();
            foreach (var value in _medikTappService.MedikTappServices)
            {
                if (!filteredItems.Contains(value))
                    AvailableServices.Remove(value);
                else if (!AvailableServices.Contains(value))
                    AvailableServices.Add(value);
            }
        }

        private void InitServices()
        {
            AvailableServices = new(_medikTappService.MedikTappServices);
            IsLoadingData = false;
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

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (_medikTappService.MedikTappServices == null || _medikTappService.MedikTappPromos == null)
            {
                _medikTappService.MedikTappServices = await _httpService.GetServices();
                _medikTappService.MedikTappPromos = await _httpService.GetPromos();
            }

            InitServices();
        }
    }
}