using MedikTapp.Enums;
using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo;
using MedikTapp.Views.Welcome.Main.TimeAvailability;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedules
{
    public partial class SchedulesTabViewModel
    {
        private async Task CancelSchedule(AppointmentModel appointment)
        {
            // Selected appointment's time violates time constraint (2 hours before the appointment)
            if (appointment.AppointmentDate.Date == DateTime.Now.Date
                && appointment.AppointmentDate.Hour > DateTime.Now.AddHours(-2).Hour)
            {
                await Application.Current.MainPage.DisplayAlert("Cancellation not available",
                        "Sorry, you can't cancel your appointment two hours before the appointment time.",
                        "OK");
                return;
            }

            var isCancelled = await Application.Current.MainPage.DisplayAlert("Appointment cancellation",
                "Are you sure you want to cancel your appointment", "Yes", "No");
            if (isCancelled)
            {
                await _httpService.CancelAppointment(appointment.AppointmentId);
                Schedules.Remove(appointment);
                _notificationService.CancelByNotificationIds(appointment.AppointmentId);
                _mainThread.BeginInvokeOnMainThread(() => _toast.Show("Appointment cancelled."));
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
        }

        private Task Reschedule(AppointmentModel appointment)
        {
            //if (appointment.AppointmentDate.Date == DateTime.Now.Date
            //    && appointment.AppointmentDate.Hour > DateTime.Now.AddHours(-2).Hour)

            // Selected appointment's time violates time constraint (2 hours before the appointment)
            return appointment.AppointmentDate.Date == DateTime.Now.Date
                && appointment.AppointmentDate.Hour > DateTime.Now.AddHours(-2).Hour
                ? Application.Current.MainPage.DisplayAlert("Reschedule not available",
                        "Sorry, you can't reschedule your appointment two hours before the appointment time.",
                        "OK")
                : NavigationService.GoTo<TimeAvailabilityPage>(new()
                {
                    { "appointment", appointment },
                    { "isResched", true },
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