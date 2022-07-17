using MedikTapp.Enums;
using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPageViewModel
    {
        private async void AppointmentFieldsChanged()
        {
            TimeCollection = new(await _httpService.GetAppointmentAvailableTime(_passedService.ServiceId,
                SelectedDate.Year, SelectedDate.Month, SelectedDate.Day));
            SelectScheduleCmd.RaiseCanExecuteChanged();
        }

        private List<DateTime> GetDisabledDates()
        {
            var disabledDates = new List<DateTime>();
            for (var date = DateTime.Now; date <= new DateTime(DateTime.Now.Year, 12, 31); date = date.AddDays(1))
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    disabledDates.Add(date);

            return disabledDates;
        }
        private void IsOnBehalfChanged()
        {
            if (!IsOnBehalf)
            {
                ProspectFirstName = "";
                ProspectLastName = "";
                ProspectAge = "";
                ProspectSex = "";
            }

            AppointmentFieldsChanged();
        }

        private bool IsSchedulingAllowed()
        {
            return IsOnBehalf
                ? SelectedDate != DateTime.MinValue
                    && SelectedTime != DateTime.MinValue
                    && !string.IsNullOrWhiteSpace(ProspectFirstName)
                    && !string.IsNullOrWhiteSpace(ProspectLastName)
                    && !string.IsNullOrWhiteSpace(ProspectSex)
                    && int.Parse(ProspectAge) > -1
                : SelectedDate != DateTime.MinValue
                    && SelectedTime != DateTime.MinValue;
        }

        private void SelectedDateChanged()
        {
            SelectedTime = DateTime.MinValue;
            AppointmentFieldsChanged();
        }

        private void SelectedTimeChanged()
        {
            SelectScheduleCmd.RaiseCanExecuteChanged();
        }

        private async Task SelectSchedule()
        {
            var schedule = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
                SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);
            await _httpService.AddAppointment(new AddAppointmentModel
            {
                AppointmentDate = schedule,
                BookingStatus = BookingStatus.Confirmed.ToString(),
                InBehalf = IsOnBehalf,
                PatientId = _appConfigService.PatientId,
                ProspectGender = ProspectSex,
                ProspectLastName = ProspectLastName,
                ProspectAge = Convert.ToInt32(ProspectAge),
                ProspectFirstName = ProspectFirstName,
                ServiceId = _passedService.ServiceId
            });

            await Task.WhenAll
            (
                _databaseService.Delete<Models.Services>(_passedService.LocalServiceId),
                _notificationService.SendLocalNotification(_passedService.ServiceId, _passedService.ServiceId,
                "You have an incoming appointment!",
                $"{_passedService.ServiceName}\n{schedule:MMMM dd, yyyy} | {schedule:hh:mm tt}",
                schedule.AddHours(-1)),
                _mainThread.InvokeOnMainThreadAsync(() => NavigationService.PopPage())
            );

            _mainThread.BeginInvokeOnMainThread(() => _toast.Show(_isRescheduled
                ? "You have successfully rescheduled your appointment."
                : "You have successfully booked an appointment."));
        }

        public override async void Initialized(NavigationParameters parameters)
        {
            //todo here
            _passedService = parameters.GetValue<Models.Services>("booking");
            _isRescheduled = parameters.GetValue<bool>("isResched"); //todo, include passed time based on sched here

            DisabledDates = GetDisabledDates();
            SelectedDate = DateTime.Now; //todo, include passed time based on sched here
            TimeCollection = new(await _httpService.GetAppointmentAvailableTime(_passedService.ServiceId,
                SelectedDate.Year, SelectedDate.Month, SelectedDate.Day));
        }
    }
}