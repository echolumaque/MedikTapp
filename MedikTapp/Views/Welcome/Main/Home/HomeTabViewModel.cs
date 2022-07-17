using MedikTapp.Helpers.Command;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private bool _isAlreadyTimed;
        private readonly MedikTappService _medikTappService;
        private readonly NotificationService _notificationService;

        public HomeTabViewModel(NavigationService navigationService,
            MedikTappService medikTappService,
            NotificationService notificationService) : base(navigationService)
        {
            _medikTappService = medikTappService;
            _notificationService = notificationService;

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