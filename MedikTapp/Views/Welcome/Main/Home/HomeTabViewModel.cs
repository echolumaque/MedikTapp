using MedikTapp.Helpers.Command;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
using System;
using System.Linq;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private bool _isAlreadyTimed;
        private readonly MockService _mockService;

        public HomeTabViewModel(NavigationService navigationService, MockService mockService) : base(navigationService)
        {
            _mockService = mockService;

            ServiceConfirmationCmd = new AsyncSingleCommand<Models.Services>(service =>
            {
                return NavigationService.GoTo<ServiceConfirmationPopup>(new()
                {
                    { "service", service }
                });
            });

            InitPromos();
            ServicesCollection = new(_mockService.MockServices.OrderBy(x => Guid.NewGuid()).Take(6));
        }
    }
}