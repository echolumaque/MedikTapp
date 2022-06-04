using MedikTapp.Enums;
using MedikTapp.Helpers.Command;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel : TabItemPageViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private readonly MockService _mockService;

        public BookingsTabViewModel(DatabaseService databaseService,
            MockService mockService,
            NavigationService navigationService) : base(navigationService)
        {
            _databaseService = databaseService;
            _mockService = mockService;

            AddBookingCmd = new AsyncSingleCommand<Tables.Bookings>(booking => AddBooking(booking));
            CancelBookingCmd = new AsyncSingleCommand<Tables.Bookings>(booking => CancelBooking(booking));
            ChangeFilterCmd = new Command<BookingSort>(filter => ChangeFilter(filter));
            OpenComboBoxCmd = new Command(() => IsFilterExpanded = !IsFilterExpanded);
        }
    }
}