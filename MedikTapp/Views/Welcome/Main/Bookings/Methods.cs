using MedikTapp.Enums;
using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.TimeAvailability;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel
    {
        private Task CancelBooking(Models.Services booking)
        {
            Bookings.Remove(booking);
            return _databaseService.Delete(booking);
        }

        private Task AddBooking(Models.Services booking)
        {
            return NavigationService.GoTo<TimeAvailabilityPopup>(new()
            {
                { "booking", booking },
                { "isResched", false }
            });
        }

        private void ChangeFilter(BookingSort selectedBookingSort)
        {
            if (SelectedBookingSort == selectedBookingSort) return;
            IsFilterExpanded = false;
            SelectedBookingSort = selectedBookingSort;
            BookingSortMainBoxText = selectedBookingSort.ToShortDescription();

            Bookings = new(selectedBookingSort switch
            {
                BookingSort.Ascending => Bookings.OrderBy(x => x.AvailableTime),
                BookingSort.Descending => Bookings.OrderByDescending(x => x.AvailableTime),
                _ => throw new NotImplementedException(),
            });
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await InitCollections();
        }

        private async Task InitCollections()
        {
            Bookings = new((await _databaseService.Find<Models.Services>()).Where(x => x.BookingStatus == BookingStatus.NotApplicable));

            BookingSortCollection = Enum.GetValues(typeof(BookingSort)).Cast<BookingSort>();
            SelectedBookingSort = BookingSortCollection.First();
            BookingSortMainBoxText = SelectedBookingSort.ToShortDescription();
        }
    }
}