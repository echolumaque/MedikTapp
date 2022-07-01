using MedikTapp.Helpers.Command;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private bool _isAlreadyTimed;
        private readonly HttpService _httpService;

        public HomeTabViewModel(NavigationService navigationService,
            HttpService httpService) : base(navigationService)
        {
            _httpService = httpService;

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