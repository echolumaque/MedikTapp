using MedikTapp.Helpers.Command;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.Home.Products;
using MedikTapp.Views.Welcome.Main.Home.ServiceConfirmation;
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

            GotoProductsCmd = new AsyncSingleCommand(() => NavigationService.GoTo<ProductsPage>());
            ServiceConfirmationCmd = new AsyncSingleCommand<Models.Services>(service =>
            {
                return NavigationService.GoTo<ServiceConfirmationPopup>(new()
                {
                    { "service", service }
                });
            });

            InitPromos();
            _mockService.OnMockServiceInitialized += (s, e) =>
                ServicesCollection = new(_mockService.MockServices.OrderBy(x => Guid.NewGuid()).Take(4));
        }
    }
}