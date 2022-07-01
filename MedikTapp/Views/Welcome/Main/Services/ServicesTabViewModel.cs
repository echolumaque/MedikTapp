using MedikTapp.Helpers.Command;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTabViewModel : TabItemPageViewModelBase
    {
        private readonly MockService _mockService;
        private readonly HttpService _httpService;

        public ServicesTabViewModel(NavigationService navigationService,
            HttpService httpService,
            MockService mockService) : base(navigationService)
        {
            _mockService = mockService;
            _httpService = httpService;

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