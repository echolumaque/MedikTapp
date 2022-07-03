using MedikTapp.Helpers.Command;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private bool _isAlreadyTimed;
        private readonly MedikTappService _medikTappService;

        public HomeTabViewModel(NavigationService navigationService,
            MedikTappService medikTappService) : base(navigationService)
        {
            _medikTappService = medikTappService;


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