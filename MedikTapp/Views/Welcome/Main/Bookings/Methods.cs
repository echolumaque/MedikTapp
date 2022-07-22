using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.TimeAvailability;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel
    {
        private Task AddBooking(Models.Services booking)
        {
            return NavigationService.GoTo<TimeAvailabilityPage>(new()
            {
                {
                    "appointment",
                    new AppointmentModel
                    {
                        ServiceId = booking.ServiceId,
                        ServiceName = booking.ServiceName,
                    }
                },
                { "isResched", false },
                { "serviceId", booking.LocalServiceId }
            });
        }

        private async Task CancelBooking(Models.Services booking)
        {
            Bookings.Remove(booking);
            await _databaseService.Delete(booking);
            GetBadgeCount();
        }

        private async Task InitCollections()
        {
            IsLoading = true;
            Bookings = new(await _databaseService.Find<Models.Services>());
            IsLoading = false;
        }

        public override async void GetBadgeCount()
        {
            var localBookingsCount = await _databaseService.FindCount<Models.Services>();
            CanHaveBadge = localBookingsCount > 0;
            if (!CanHaveBadge)
                return;
            BadgeCount = localBookingsCount;
        }

        public override async void Initialized(NavigationParameters parameters)
        {
            await InitCollections();
        }

        private async Task Refresh()
        {
            IsRefreshing = true;
            await InitCollections();
            IsRefreshing = false;
        }
    }
}