using MedikTapp.Enums;
using MedikTapp.Services.NavigationService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel
    {
        private Task CancelBooking(Tables.Bookings booking)
        {
            Bookings.Remove(booking);
            return _databaseService.Delete(booking);
        }

        private async Task AddBooking(Tables.Bookings booking)
        {
            //todo: upload to remote db
            await _databaseService.Insert(new Models.Schedules
            {
                BookingStatus = BookingStatus.Pending,
                ProductDescription = booking.ProductDescription,
                ProductImagePath = booking.ProductImagePath,
                ProductName = booking.ProductName,
                ProductPrice = booking.ProductPrice,
                Schedule = booking.EarliestAvailableDate,
            });
            Bookings.Remove(booking);
            await _databaseService.Delete(booking);
        }

        private void ChangeFilter(BookingSort selectedBookingSort)
        {
            if (SelectedBookingSort == selectedBookingSort) return;
            IsFilterExpanded = false;
            SelectedBookingSort = selectedBookingSort;
            BookingSortMainBoxText = selectedBookingSort.ToShortDescription();

            Bookings = new(selectedBookingSort switch
            {
                BookingSort.Ascending => Bookings.OrderBy(x => x.EarliestAvailableDate),
                BookingSort.Descending => Bookings.OrderByDescending(x => x.EarliestAvailableDate),
                _ => throw new NotImplementedException(),
            });
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await InitCollections();
        }

        private async Task InitCollections()
        {
            Bookings = new(await _databaseService.Find<Tables.Bookings>());

            BookingSortCollection = Enum.GetValues(typeof(BookingSort)).Cast<BookingSort>();
            SelectedBookingSort = BookingSortCollection.First();
            BookingSortMainBoxText = SelectedBookingSort.ToShortDescription();
        }
    }
}