using MedikTapp.Helpers.Command;
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

        public ServicesTabViewModel(NavigationService navigationService,
            MedikTappService medikTappService) : base(navigationService)
        {
            _medikTappService = medikTappService;

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