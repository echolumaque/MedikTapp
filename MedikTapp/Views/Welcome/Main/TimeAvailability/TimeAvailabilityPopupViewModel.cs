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
    public partial class TimeAvailabilityPopupViewModel : ViewModelBase
    {
        private const int ClinicMaxHourCount = 17;
        private readonly DatabaseService _databaseService;
        private readonly HttpService _httpService;
        private readonly bool _isFirstLaunched;
        private readonly IMainThread _mainThread;
        private readonly NotificationService _notificationService;
        private readonly IToast _toast;
        private bool _isRescheduled;
        private Models.Services _passedService;
        private readonly AppConfigService _appConfigService;

        public TimeAvailabilityPopupViewModel(NavigationService navigationService,
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

            SelectScheduleCmd = new AsyncSingleCommand(SelectSchedule, () => SelectedDate != null && SelectedTime != null);
        }
    }
}