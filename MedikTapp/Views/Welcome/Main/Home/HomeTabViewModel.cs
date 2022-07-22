using MedikTapp.Helpers.Command;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private readonly MedikTappService _medikTappService;
        private readonly HttpService _httpService;

        public HomeTabViewModel(NavigationService navigationService,
            HttpService httpService,
            MedikTappService medikTappService) : base(navigationService)
        {
            _medikTappService = medikTappService;
            _httpService = httpService;

            _medikTappService.MedikTappServiceInitialized += OnMedikTappServiceInitialized;
            ServiceConfirmationCmd = new AsyncSingleCommand<Models.Services>(GotoServiceConfirmationPopup);
            RefreshCmd = new AsyncSingleCommand(Refresh);
        }
    }
}