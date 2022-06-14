using MedikTapp.Enums;
using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo;
using MedikTapp.Views.Welcome.Main.TimeAvailability;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTabViewModel
    {
        public override async void Initialized(NavigationParameters parameters)
        {
            await InitUpcomingCollections();

            BookingSortCollection = Enum.GetValues(typeof(BookingSort)).Cast<BookingSort>();
            SelectedBookingSort = BookingSortCollection.First();
            BookingSortMainBoxText = SelectedBookingSort.ToShortDescription();
        }

        private Task CancelSchedule(Models.Services schedule)
        {
            if (DateTime.Now.Date == schedule.AvailableTime.AddDays(-3).Date)
            {
                return Application.Current.MainPage.DisplayAlert("Cancellation not available",
                    "Sorry, you can't cancel your appointment three days before the selected date.",
                    "OK");
            }
            else
            {
                Schedules.Remove(schedule);
                _toast.Show("Appointment cancelled.");
                //todo remove to remote db
                return _databaseService.Update(new Models.Services
                {
                    AvailableTime = schedule.AvailableTime,
                    BookingStatus = BookingStatus.Cancelled,
                    ServiceDescription = schedule.ServiceDescription,
                    ServiceId = schedule.ServiceId,
                    ServiceImagePath = schedule.ServiceImagePath,
                    ServiceName = schedule.ServiceName,
                    ServicePrice = schedule.ServicePrice
                });
            }
        }

        private Task Reschedule(Models.Services schedule)
        {
            return NavigationService.GoTo<TimeAvailabilityPopup>(new()
            {
                { "booking", schedule },
                { "isResched", true }
            });
        }

        private Task ServiceTapped(Models.Services service)
        {
            return NavigationService.GoTo<ServiceInfoPopup>(new()
            {
                { "service", service }
            });
        }

        private async Task InitUpcomingCollections()
        {
            SelectedBookingStatus = BookingStatus.Confirmed;

            var scheds = await _databaseService.Find<Models.Services>();
            Schedules = new(scheds.Where(x => x.BookingStatus == BookingStatus.Confirmed
                || x.BookingStatus == BookingStatus.Pending));
        }

        private async Task InitCompletedCollections()
        {
            SelectedBookingStatus = BookingStatus.Completed;

            var scheds = await _databaseService.Find<Models.Services>();
            Schedules = new(scheds.Where(x => x.BookingStatus == BookingStatus.Completed));
        }

        private async Task InitCancelledCollections()
        {
            SelectedBookingStatus = BookingStatus.Cancelled;

            var scheds = await _databaseService.Find<Models.Services>();
            Schedules = new(scheds.Where(x => x.BookingStatus == BookingStatus.Cancelled));
        }

        private async Task ChangeFilter(BookingSort selectedBookingSort)
        {
            if (SelectedBookingSort == selectedBookingSort) return;
            IsFilterExpanded = false;
            SelectedBookingSort = selectedBookingSort;
            BookingSortMainBoxText = selectedBookingSort.ToShortDescription();
            var _schedules = await _databaseService.Find<Models.Services>();

            Schedules = new((selectedBookingSort, SelectedBookingStatus) switch
            {
                //confirmed or pendiong
                (BookingSort.Ascending, BookingStatus.Confirmed or BookingStatus.Pending) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed || x.BookingStatus == BookingStatus.Pending)
                    .OrderBy(x => x.AvailableTime),
                (BookingSort.Descending, BookingStatus.Confirmed or BookingStatus.Pending) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed || x.BookingStatus == BookingStatus.Pending)
                    .OrderByDescending(x => x.AvailableTime),

                //completed
                (BookingSort.Ascending, BookingStatus.Completed) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Completed)
                    .OrderBy(x => x.AvailableTime),
                (BookingSort.Descending, BookingStatus.Completed) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Completed)
                    .OrderByDescending(x => x.AvailableTime),

                //cancelled
                (BookingSort.Ascending, BookingStatus.Cancelled) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled)
                    .OrderBy(x => x.AvailableTime),
                (BookingSort.Descending, BookingStatus.Cancelled) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled)
                    .OrderByDescending(x => x.AvailableTime),

                _ => throw new NotImplementedException(),
            });
        }
    }
}
