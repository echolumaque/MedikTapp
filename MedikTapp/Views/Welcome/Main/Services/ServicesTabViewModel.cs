using MedikTapp.Helpers.Command;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTabViewModel : TabItemPageViewModelBase
    {
        private readonly MedikTappService _medikTappService;
        private readonly HttpService _httpService;

        public ServicesTabViewModel(NavigationService navigationService,
            MedikTappService medikTappService,
            HttpService httpService) : base(navigationService)
        {
            _medikTappService = medikTappService;
            _httpService = httpService;
            medikTappService.MedikTappServiceInitialized += OnMedikTappServiceInitialized;

            RefreshCmd = new AsyncSingleCommand(Refresh);
            SearchEntryTextChangedCmd = new Command<TextChangedEventArgs>(args => FilterServices(args));
            ServiceConfirmationCmd = new AsyncSingleCommand<Models.Services>(service =>
            {
                return NavigationService.GoTo<ServiceConfirmationPopup>(new()
                {
                    { "service", service }
                });
            });
        }
    }
}