using MedikTapp.Enums;
using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTabViewModel
    {
        public override async void Initialized(NavigationParameters parameters)
        {
            _schedules = await _databaseService.Find<Models.Schedules>();
            Schedules = new(_schedules);
            InitUpcomingCollections();

            BookingSortCollection = Enum.GetValues(typeof(BookingSort)).Cast<BookingSort>();
            SelectedBookingSort = BookingSortCollection.First();
            BookingSortMainBoxText = SelectedBookingSort.ToShortDescription();
        }

        private Task CancelSchedule(Schedules schedule)
        {
            Schedules.Remove(schedule);
            //remove to remote db
            return _databaseService.Delete(schedule);
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
                    .OrderBy(x => x.Schedule),
                (BookingSort.Descending, BookingStatus.Confirmed or BookingStatus.Pending) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed || x.BookingStatus == BookingStatus.Pending)
                    .OrderByDescending(x => x.Schedule),

                //completed
                (BookingSort.Ascending, BookingStatus.Completed) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Completed)
                    .OrderBy(x => x.Schedule),
                (BookingSort.Descending, BookingStatus.Completed) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Completed)
                    .OrderByDescending(x => x.Schedule),

                //cancelled
                (BookingSort.Ascending, BookingStatus.Cancelled) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled)
                    .OrderBy(x => x.Schedule),
                (BookingSort.Descending, BookingStatus.Cancelled) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled)
                    .OrderByDescending(x => x.Schedule),

                _ => throw new NotImplementedException(),
            });
        }
    }
}
