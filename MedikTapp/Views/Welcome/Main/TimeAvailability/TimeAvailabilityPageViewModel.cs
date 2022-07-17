using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Essentials.Interfaces;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPageViewModel : ViewModelBase
    {
        private readonly AppConfigService _appConfigService;
        private readonly DatabaseService _databaseService;
        private readonly HttpService _httpService;
        private readonly IMainThread _mainThread;
        private readonly NotificationService _notificationService;
        private readonly IToast _toast;
        private bool _isRescheduled;
        private Models.Services _passedService;

        public TimeAvailabilityPageViewModel(NavigationService navigationService,
            DatabaseService databaseService,
            NotificationService notificationService,
            IToast toast,
            HttpService httpService,
            AppConfigService appConfigService,
            IMainThread mainThread)
            : base(navigationService)
        {
            _appConfigService = appConfigService;
            _databaseService = databaseService;
            _notificationService = notificationService;
            _toast = toast;
            _httpService = httpService;
            _mainThread = mainThread;

            SelectScheduleCmd = new AsyncSingleCommand(SelectSchedule, () => IsSchedulingAllowed());
        }
    }
}