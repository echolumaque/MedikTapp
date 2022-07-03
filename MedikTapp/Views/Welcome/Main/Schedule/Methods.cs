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
            _schedules = await _httpService.GetAppointmentsByPatientId(_appConfigService.PatientId);
            InitUpcomingCollections();

            BookingSortCollection = Enum.GetValues(typeof(BookingSort)).Cast<BookingSort>();
            SelectedBookingSort = BookingSortCollection.First();
            BookingSortMainBoxText = SelectedBookingSort.ToShortDescription();
        }

        private async Task GetBadgeCount()
        {
            var schedules = await _httpService.GetAppointmentsByPatientId(_appConfigService.PatientId);
            BadgeCount = schedules.Count();
        }

        private async Task CancelSchedule(Models.ScheduleModel schedule)
        {
            if ((int)schedule.AppointmentDate.Date.Subtract(DateTime.Now).TotalDays <= 3)
            {
                await Application.Current.MainPage.DisplayAlert("Cancellation not available",
                    "Sorry, you can't cancel your appointment three days before the appointment date.",
                    "OK");
            }
            else
            {
                var isCancelled = await Application.Current.MainPage.DisplayAlert("Appointment cancellation",
                    "Are you sure you want to cancel your appointment", "Yes", "No");
                if (isCancelled)
                {
                    //Schedules.Remove(schedule);
                    _toast.Show("Appointment cancelled.");
                    _notificationService.CancelByNotificationIds(schedule.ServiceId);
                    //todo remove to remote db
                    //await _databaseService.Update(new Models.Services
                    //{
                    //    AvailableTime = schedule.AvailableTime,
                    //    BookingStatus = BookingStatus.Cancelled,
                    //    ServiceDescription = schedule.ServiceDescription,
                    //    ServiceId = schedule.ServiceId,
                    //    ServiceImage = schedule.ServiceImage,
                    //    ServiceName = schedule.ServiceName,
                    //    ServicePrice = schedule.ServicePrice
                    //});
                }
            }
        }

        private Task Reschedule(Models.ScheduleModel schedule)
        {
            if ((int)schedule.AppointmentDate.Date.Subtract(DateTime.Now).TotalDays <= 3)
            {
                return Application.Current.MainPage.DisplayAlert("Reschedule not available",
                    "Sorry, you can't reschedule your appointment three days before the appointment date.",
                    "OK");
            }
            else
            {
                return NavigationService.GoTo<TimeAvailabilityPopup>(new()
                {
                    { "booking", schedule },
                    { "isResched", true }
                });
            }
        }

        private Task ServiceTapped(Models.ScheduleModel service)
        {
            return NavigationService.GoTo<ServiceInfoPopup>(new()
            {
                { "service", service }
            });
        }

        private void InitUpcomingCollections()
        {
            SelectedBookingStatus = BookingStatus.Confirmed;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed.ToString()
                || x.BookingStatus == BookingStatus.Pending.ToString()).OrderBy(x => x.AppointmentDate));
            BadgeCount = Schedules.Count;
        }

        private void InitCompletedCollections()
        {
            SelectedBookingStatus = BookingStatus.Completed;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Completed.ToString())
                .OrderBy(x => x.AppointmentDate));
        }

        private void InitCancelledCollections()
        {
            SelectedBookingStatus = BookingStatus.Cancelled;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled.ToString())
                .OrderBy(x => x.AppointmentDate));
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
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed.ToString()
                        || x.BookingStatus == BookingStatus.Pending.ToString())
                        .OrderBy(x => x.AppointmentDate),
                (BookingSort.Descending, BookingStatus.Confirmed or BookingStatus.Pending) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed.ToString()
                        || x.BookingStatus == BookingStatus.Pending.ToString())
                    .OrderByDescending(x => x.AppointmentDate),

                //completed
                (BookingSort.Ascending, BookingStatus.Completed) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Completed.ToString())
                    .OrderBy(x => x.AppointmentDate),
                (BookingSort.Descending, BookingStatus.Completed) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Completed.ToString())
                    .OrderByDescending(x => x.AppointmentDate),

                //cancelled
                (BookingSort.Ascending, BookingStatus.Cancelled) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled.ToString())
                    .OrderBy(x => x.AppointmentDate),
                (BookingSort.Descending, BookingStatus.Cancelled) =>
                    _schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled.ToString())
                    .OrderByDescending(x => x.AppointmentDate),

                _ => throw new NotImplementedException(),
            });
        }
    }
}