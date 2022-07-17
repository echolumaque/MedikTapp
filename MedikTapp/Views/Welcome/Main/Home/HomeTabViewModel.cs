using MedikTapp.Helpers.Command;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;

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

            ServiceConfirmationCmd = new AsyncSingleCommand<Models.Services>(GotoServiceConfirmationPopup);
        }
    }
}