using MedikTapp.Helpers.Command;
using MedikTapp.Models;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;

namespace MedikTapp.Views.Welcome.Main.Schedule.Calendar
{
    public partial class CalendarPopupViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private Schedules _passedSchedule;

        public CalendarPopupViewModel(NavigationService navigationService, DatabaseService databaseService) : base(navigationService)
        {
            _databaseService = databaseService;

            RescheduleCmd = new AsyncSingleCommand(Reschedule);
        }
    }
}