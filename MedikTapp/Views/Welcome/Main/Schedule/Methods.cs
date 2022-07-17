using MedikTapp.Enums;
using MedikTapp.Models;
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
        private async Task CancelSchedule(AppointmentModel appointment)
        {
            if ((int)appointment.AppointmentDate.Date.Subtract(DateTime.Now).TotalDays <= 3)
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
                    //todo here
                    //Schedules.Remove(schedule);
                    _toast.Show("Appointment cancelled.");
                    //_notificationService.CancelByNotificationIds(appointment.);
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

        private void ChangeFilter(BookingSort selectedBookingSort)
        {
            if (SelectedBookingSort == selectedBookingSort)
                return;
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

        private void InitCancelledCollections()
        {
            SelectedBookingStatus = BookingStatus.Cancelled;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Cancelled.ToString())
                .OrderBy(x => x.AppointmentDate));
        }

        private void InitCompletedCollections()
        {
            SelectedBookingStatus = BookingStatus.Completed;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Completed.ToString())
                .OrderBy(x => x.AppointmentDate));
        }

        private void InitUpcomingCollections()
        {
            SelectedBookingStatus = BookingStatus.Confirmed;
            Schedules = new(_schedules.Where(x => x.BookingStatus == BookingStatus.Confirmed.ToString()
                || x.BookingStatus == BookingStatus.Pending.ToString()).OrderBy(x => x.AppointmentDate));
            BadgeCount = Schedules.Count;
        }

        private Task Reschedule(AppointmentModel appointment)
        {
            //todo here
            return (int)appointment.AppointmentDate.Date.Subtract(DateTime.Now).TotalDays <= 3
                ? Application.Current.MainPage.DisplayAlert("Reschedule not available",
                    "Sorry, you can't reschedule your appointment three days before the appointment date.",
                    "OK")
                : NavigationService.GoTo<TimeAvailabilityPage>(new()
                {
                    { "booking", appointment },
                    { "isResched", true }
                });
        }

        private Task ServiceTapped(AppointmentModel appointment)
        {
            return NavigationService.GoTo<ServiceInfoPopup>(new()
            {
                { "appointment", appointment }
            });
        }

        public override async void GetBadgeCount()
        {
            var schedules = _schedules ?? await _httpService.GetAppointmentsByPatientId(_appConfigService.PatientId);
            BadgeCount = schedules.Count();
        }

        public override async void Initialized(NavigationParameters parameters)
        {
            BookingSortCollection = Enum.GetValues(typeof(BookingSort)).Cast<BookingSort>();
            SelectedBookingSort = BookingSortCollection.First();
            BookingSortMainBoxText = SelectedBookingSort.ToShortDescription();

            _schedules = await _httpService.GetAppointmentsByPatientId(_appConfigService.PatientId);
            InitUpcomingCollections();
            GetBadgeCount();
        }
    }
}