using MedikTapp.Services.NavigationService;
using System.Linq;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTabViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            AvailableServices = new(_medikTappService.MedikTappServices);
            IsLoadingData = false;
        }

        private void FilterServices(TextChangedEventArgs args)
        {
            var searchTerm = args.NewTextValue.ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(searchTerm)) searchTerm = string.Empty;

            var filteredItems = _medikTappService.MedikTappServices.Where(x => x.ServiceName.ToLowerInvariant().Contains(searchTerm)).ToList();
            foreach (var value in _medikTappService.MedikTappServices)
            {
                if (!filteredItems.Contains(value))
                    AvailableServices.Remove(value);
                else if (!AvailableServices.Contains(value))
                    AvailableServices.Add(value);
            }
        }
    }
}