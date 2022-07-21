using MedikTapp.Helpers.Command;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Essentials.Interfaces;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPageViewModel : ViewModelBase
    {
        private readonly ILauncher _launcher;
        private readonly IGeolocation _geolocation;

        public ServiceInfoPageViewModel(NavigationService navigationService,
            ILauncher launcher,
            IGeolocation geolocation) : base(navigationService)
        {
            _launcher = launcher;
            _geolocation = geolocation;

            OpenMapCmd = new AsyncSingleCommand(OpenMap);
        }
    }
}