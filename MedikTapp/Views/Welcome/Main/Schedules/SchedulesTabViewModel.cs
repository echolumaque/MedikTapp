using MedikTapp.Enums;
using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Models;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using System.Collections.Generic;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedules
{
    public partial class SchedulesTabViewModel : TabItemPageViewModelBase
    {
        private readonly AppConfigService _appConfigService;
        private readonly HttpService _httpService;
        private readonly IMainThread _mainThread;
        private readonly NotificationService _notificationService;
        private readonly IToast _toast;
        private IEnumerable<AppointmentModel> _schedules;

        public SchedulesTabViewModel(NavigationService navigationService,
            AppConfigService appConfigService,
            HttpService httpService,
            IMainThread mainThread,
            NotificationService notificationService,
            IToast toast) : base(navigationService)
        {
            _notificationService = notificationService;
            _toast = toast;
            _httpService = httpService;
            _mainThread = mainThread;
            _appConfigService = appConfigService;

            ChangeFilterCmd = new Command<BookingSort>(ChangeFilter);
            FilterCancelledCmd = new Command(InitCancelledCollections);
            FilterCompletedCmd = new Command(InitCompletedCollections);
            FilterUpcomingCmd = new Command(InitUpcomingCollections);
            OpenComboBoxCmd = new Command(() => IsFilterExpanded = !IsFilterExpanded);
            CancelScheduleCmd = new AsyncSingleCommand<AppointmentModel>(CancelSchedule);
            RescheduleCmd = new AsyncSingleCommand<AppointmentModel>(Reschedule);
            ServiceTappedCmd = new AsyncSingleCommand<AppointmentModel>(ServiceTapped);
        }
    }
}