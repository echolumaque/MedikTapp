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
                { "booking", booking },
                { "isResched", false }
            });
        }

        private Task CancelBooking(Models.Services booking)
        {
            Bookings.Remove(booking);
            return _databaseService.Delete(booking);
        }

        private async Task InitCollections()
        {
            Bookings = new(await _databaseService.Find<Models.Services>());
        }

        public override async void GetBadgeCount()
        {
            BadgeCount = await _databaseService.FindCount<Models.Services>();
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await InitCollections();
            GetBadgeCount();
        }
    }
}