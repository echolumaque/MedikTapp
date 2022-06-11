using MedikTapp.Enums;
using MedikTapp.Helpers.Command;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTabViewModel : TabItemPageViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private readonly MockService _mockService;
        private IEnumerable<Models.Services> _schedules;

        public ScheduleTabViewModel(NavigationService navigationService,
            MockService mockService,
            DatabaseService databaseService) : base(navigationService)
        {
            _databaseService = databaseService;
            _mockService = mockService;

            ChangeFilterCmd = new Command<BookingSort>(filter => ChangeFilter(filter));
            FilterCancelledCmd = new Command(InitCancelledCollections);
            FilterCompletedCmd = new Command(InitCompletedCollections);
            FilterUpcomingCmd = new Command(InitUpcomingCollections);
            OpenComboBoxCmd = new Command(() => IsFilterExpanded = !IsFilterExpanded);
            CancelScheduleCmd = new AsyncSingleCommand<Models.Services>(CancelSchedule);
            RescheduleCmd = new AsyncSingleCommand<Models.Services>(Reschedule);
            ServiceTappedCmd = new AsyncSingleCommand<Models.Services>(ServiceTapped);
        }
    }
}