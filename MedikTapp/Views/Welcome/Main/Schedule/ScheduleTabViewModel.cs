using MedikTapp.Enums;
using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTabViewModel : TabItemPageViewModelBase
    {
        private readonly NotificationService _notificationService;
        private readonly AppConfigService _appConfigService;
        private readonly HttpService _httpService;
        private readonly IToast _toast;
        private IEnumerable<Models.ScheduleModel> _schedules;

        public ScheduleTabViewModel(NavigationService navigationService,
            AppConfigService appConfigService,
            HttpService httpService,
            NotificationService notificationService,
            IToast toast) : base(navigationService)
        {
            _notificationService = notificationService;
            _toast = toast;
            _httpService = httpService;
            _appConfigService = appConfigService;

            ChangeFilterCmd = new Command<BookingSort>(ChangeFilter);
            FilterCancelledCmd = new Command(InitCancelledCollections);
            FilterCompletedCmd = new Command(InitCompletedCollections);
            FilterUpcomingCmd = new Command(InitUpcomingCollections);
            OpenComboBoxCmd = new Command(() => IsFilterExpanded = !IsFilterExpanded);
            CancelScheduleCmd = new AsyncSingleCommand<Models.ScheduleModel>(CancelSchedule);
            RescheduleCmd = new AsyncSingleCommand<Models.ScheduleModel>(Reschedule);
            ServiceTappedCmd = new AsyncSingleCommand<Models.ScheduleModel>(ServiceTapped);

            GetBadgeCount().ConfigureAwait(false);
        }
    }
}