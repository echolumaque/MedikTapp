using MedikTapp.Helpers.Command;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel : TabItemPageViewModelBase
    {
        private readonly DatabaseService _databaseService;

        public BookingsTabViewModel(DatabaseService databaseService,
            NavigationService navigationService) : base(navigationService)
        {
            _databaseService = databaseService;

            ScheduleCmd = new AsyncSingleCommand<Models.Services>(AddBooking);
            CancelBookingCmd = new AsyncSingleCommand<Models.Services>(CancelBooking);
            RefreshCmd = new AsyncSingleCommand(Refresh);
        }
    }
}