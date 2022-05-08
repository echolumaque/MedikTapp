using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.Home.Products;
using MedikTapp.Views.Welcome.Main.Home.ServiceConfirmation;
using System;
using System.Linq;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private bool _isAlreadyTimed;
        private readonly MockService _mockService;

        public HomeTabViewModel(NavigationService navigationService, MockService mockService) : base(navigationService)
        {
            _mockService = mockService;

            GotoProductsCmd = new AsyncCommand(() => NavigationService.GoTo<ProductsPage>(), allowsMultipleExecutions: false);
            ServiceConfirmationCmd = new AsyncCommand<Models.Services>(service =>
            {
                return NavigationService.GoTo<ServiceConfirmationPopup>(new()
                {
                    { "service", service }
                });
            }, allowsMultipleExecutions: false);

            InitPromos();
            _mockService.OnMockServiceInitialized += (s, e) =>
                ServicesCollection = new(_mockService.MockServices.OrderBy(x => Guid.NewGuid()).Take(4));
        }
    }
}