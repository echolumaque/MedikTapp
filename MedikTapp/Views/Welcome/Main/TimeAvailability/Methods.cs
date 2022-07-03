using MedikTapp.Enums;
using MedikTapp.Services.NavigationService;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel
    {
        private List<DateTime> GetDisabledDates()
        {
            var disabledDates = new List<DateTime>();
            for (var date = DateTime.Now; date <= new DateTime(DateTime.Now.Year, 12, 31); date = date.AddDays(1))
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    disabledDates.Add(date);

            return disabledDates;
        }

        public override void Initialized(NavigationParameters parameters)
        {
            _passedService = parameters.GetValue<Models.Services>("booking");
            _isRescheduled = parameters.GetValue<bool>("isResched");
            CustomDayLabels = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
            DisabledDates = GetDisabledDates();
            SelectedDate = _passedService.AvailableTime == default ? DateTime.Now : _passedService.AvailableTime;
        }

        private async Task PopulateTime(bool isTodayDate)
        {
            if (SelectedDate.DayOfWeek != DayOfWeek.Sunday)
            {
                if (isTodayDate)
                {
                    var availableTimeFromAzure = await GetAvailableTime();

                    var startAllowedTime = DateTime.Now.Hour + 2;
                    var allowedTimes = Enumerable.Range(startAllowedTime, ClinicMaxHourCount - startAllowedTime);
                    var allowedTimeOffset = new List<DateTime>();
                    foreach (var time in allowedTimes)
                        allowedTimeOffset.Add(DateTime.Today.Add(new TimeSpan(time, 0, 0)));

                    TimeCollection = new(availableTimeFromAzure.Intersect(allowedTimeOffset));
                }
                else
                    TimeCollection = new(await GetAvailableTime());
            }
        }

        private async Task<IEnumerable<DateTime>> GetAvailableTime()
        {
            var allowedTimes = await _httpService
                .GetAppointmentAvailableTime(_passedService.ServiceId, SelectedDate.Year, SelectedDate.Month, SelectedDate.Day);
            var availableTimes = new List<DateTime>();
            foreach (var time in allowedTimes)
            {
                availableTimes.Add(time);
            }

            return availableTimes;
        }


        private async void RaiseSelectScheduleCanExecuteChanged()
        {
            await PopulateTime(SelectedDate.Date == DateTime.Now.Date);
            SelectScheduleCmd.RaiseCanExecuteChanged();
        }

        private async Task SelectSchedule()
        {
            var schedule = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
                SelectedTime.Hour, SelectedTime.Minute, SelectedTime.Second);
            await _httpService.AddAppointment(new()
            {
                AppointmentDate = schedule,
                BookingStatus = BookingStatus.Confirmed.ToString(),
                PatientId = _appConfigService.PatientId,
                ServiceId = _passedService.ServiceId
            });

            await Task.WhenAll
            (
                _databaseService.Delete<Models.Services>(_passedService.LocalServiceId),
                _notificationService.Send(_passedService.ServiceId, "You have an incoming appointment!", schedule.AddHours(-1),
                $"{_passedService.ServiceName}\n{schedule:MMMM dd, yyyy} | {schedule:hh:mm tt}",
                categoryType: NotificationCategoryType.Reminder,
                androidSpecificOptions: new()
                {
                    ChannelId = _passedService.ServiceId.ToString(),
                    Group = "MedikTapp",
                    IsGroupSummary = true,
                    IconLargeName = new AndroidIcon
                    {
                        ResourceName = "mediktapp_notif_icon",
                    },
                    IconSmallName = new AndroidIcon
                    {
                        ResourceName = "mediktapp_notif_icon",
                    },
                    Priority = AndroidNotificationPriority.Max,
                    VisibilityType = AndroidVisibilityType.Public,
                }),

                NavigationService.PopPopup()
            );

            _mainThread.BeginInvokeOnMainThread(() => _toast.Show(_isRescheduled
                ? "You have successfully rescheduled your appointment."
                : "You have successfully booked an appointment."));
        }
    }
}