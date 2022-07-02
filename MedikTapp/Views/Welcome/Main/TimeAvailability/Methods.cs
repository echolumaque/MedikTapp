using MedikTapp.Services.NavigationService;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel
    {
        private List<DateTime> GetDisabledDates()
        {
            var disabledDates = new List<DateTime>();

            for (var date = DateTime.Now; date <= new DateTime(DateTime.Now.Year, 12, 31); date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    disabledDates.Add(date);
            }
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

        private void OnSelectedTimeChanged(SelectionChangedEventArgs args)
        {
            if (args.CurrentSelection.Count < 1)
                return;

            var selectedtime = (Models.TimeAvailability)args.CurrentSelection[0];
            if (!selectedtime.IsAvailable)
                SelectedTime = null;
        }

        private void PopulateTime(bool isTodayDate)
        {
            var startAllowedTime = isTodayDate ? DateTime.Now.Hour + 2 : 7;
            var allowedTimes = Enumerable.Range(startAllowedTime, ClinicMaxHourCount - startAllowedTime);
            TimeCollection = new();
            foreach (var time in allowedTimes)
            {
                TimeCollection.Add(new()
                {
                    IsAvailable = true, //todo: check on backend first
                    Time = DateTime.Today.Add(new TimeSpan(time, 0, 0))
                });
            }
        }

        private void RaiseSelectScheduleCanExecuteChanged()
        {
            if (SelectedDate.Date == DateTime.Now.Date)
            {
                if (DateTime.Now.Hour < 15)
                    // If current time is less than 3pm, populate the time collection
                    PopulateTime(true);
                else
                    // If current time is more than 3pm, clear the time collection to prevent time selection by creating new empty instance
                    TimeCollection = new();
            }
            else
                // If the selected date is in the future, populate the time collection from 7am to 4pm
                PopulateTime(false);


            SelectScheduleCmd.RaiseCanExecuteChanged();
        }

        private async Task SelectSchedule()
        {
            var schedule = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Time.Hour, SelectedTime.Time.Minute, SelectedTime.Time.Second);
            await _databaseService.Update(new Models.Services
            {
                AvailableTime = schedule,
                BookingStatus = Enums.BookingStatus.Confirmed,
                ServiceDescription = _passedService.ServiceDescription,
                ServiceId = _passedService.ServiceId,
                ServiceImage = _passedService.ServiceImage,
                ServiceName = _passedService.ServiceName,
                ServicePrice = _passedService.ServicePrice
            });

            await Task.WhenAll
            (
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

            _toast.Show(_isRescheduled
                ? "You have successfully rescheduled your appointment."
                : "You have successfully booked an appointment.");
        }
    }
}