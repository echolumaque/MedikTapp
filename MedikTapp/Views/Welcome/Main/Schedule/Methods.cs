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
            _schedules = await _databaseService.Find<Models.Services>();
            Schedules = new(_schedules);
            InitUpcomingCollections();

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
                //todo remove to remote db
                return _databaseService.Delete(schedule);
            }
        }

        private Task Reschedule(Models.Services schedule)
        {
            return NavigationService.GoTo<TimeAvailabilityPopup>(new()
            {
                { "booking", schedule }
            });
        }

        private Task ServiceTapped(Models.Services service)
        {
            return NavigationService.GoTo<ServiceInfoPopup>(new()
            {
                { "service", service }
            });
        }

        private void InitUpcomingCollections()
        {
            SelectedBookingStatus = BookingStatus.Confirmed;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed
                || x.BookingStatus == BookingStatus.Pending));
        }

        private void InitCompletedCollections()
        {
            SelectedBookingStatus = BookingStatus.Completed;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Completed));
        }

        private void InitCancelledCollections()
        {
            SelectedBookingStatus = BookingStatus.Cancelled;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled));
        }

        private void ChangeFilter(BookingSort selectedBookingSort)
        {
            if (SelectedBookingSort == selectedBookingSort) return;
            IsFilterExpanded = false;
            SelectedBookingSort = selectedBookingSort;
            BookingSortMainBoxText = selectedBookingSort.ToShortDescription();

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
