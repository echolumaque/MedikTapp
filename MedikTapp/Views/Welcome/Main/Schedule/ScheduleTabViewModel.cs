using MedikTapp.Enums;
using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTabViewModel : TabItemPageViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private readonly NotificationService _notificationService;
        private readonly IToast _toast;

        public ScheduleTabViewModel(NavigationService navigationService,
            DatabaseService databaseService,
            NotificationService notificationService,
            IToast toast) : base(navigationService)
        {
            _databaseService = databaseService;
            _notificationService = notificationService;
            _toast = toast;

            ChangeFilterCmd = new AsyncSingleCommand<BookingSort>(ChangeFilter);
            FilterCancelledCmd = new AsyncSingleCommand(InitCancelledCollections);
            FilterCompletedCmd = new AsyncSingleCommand(InitCompletedCollections);
            FilterUpcomingCmd = new AsyncSingleCommand(InitUpcomingCollections);
            OpenComboBoxCmd = new Command(() => IsFilterExpanded = !IsFilterExpanded);
            CancelScheduleCmd = new AsyncSingleCommand<Models.Services>(CancelSchedule);
            RescheduleCmd = new AsyncSingleCommand<Models.Services>(Reschedule);
            ServiceTappedCmd = new AsyncSingleCommand<Models.Services>(ServiceTapped);
        }
    }
}