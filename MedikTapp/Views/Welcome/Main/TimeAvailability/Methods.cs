using MedikTapp.Services.NavigationService;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel
    {
        private Models.Services _passedService;

        private void RaiseSelectScheduleCanExecuteChanged() => SelectScheduleCmd.RaiseCanExecuteChanged();

        public override void Initialized(NavigationParameters parameters)
        {
            _passedService = parameters.GetValue<Models.Services>("booking");
            CustomDayLabels = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
            DisabledDates = GetDisabledDates();
            SelectedDate = _passedService.AvailableTime;

            TimeCollection = new();
            for (var index = 7; index < 17; index++)
            {
                TimeCollection.Add(new()
                {
                    IsAvailable = true,
                    Time = DateTime.Today.Add(new TimeSpan(index, 0, 0))
                });
            }
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
                ServiceImagePath = _passedService.ServiceImagePath,
                ServiceName = _passedService.ServiceName,
                ServicePrice = _passedService.ServicePrice
            });

            await Task.WhenAll
            (
                _notificationService.Send(_passedService.ServiceId, "MedikTapp", schedule.AddHours(-1),
                $"You have an incoming appointment!\n{_passedService.ServiceName}\n{schedule:MMMM dd, yyyy} | {schedule:hh:mm tt}",
                categoryType: NotificationCategoryType.Reminder,
                androidSpecificOptions: new()
                {
                    ChannelId = _passedService.ServiceId.ToString(),
                    Group = "MedikTapp",
                    IsGroupSummary = true,
                    Priority = AndroidNotificationPriority.Max,
                    VisibilityType = AndroidVisibilityType.Public
                }),

                NavigationService.PopPopup()
            );
        }

        private void OnSelectedTimeChanged(SelectionChangedEventArgs args)
        {
            if (args.CurrentSelection.Count < 1)
                return;

            var selectedtime = (Models.TimeAvailability)args.CurrentSelection[0];
            if (!selectedtime.IsAvailable)
                SelectedTime = null;
        }

        private List<DateTime> GetDisabledDates()
        {
            var disabledDates = new List<DateTime>();
            for (var date = DateTime.Now; date <= new DateTime(2022, 12, 31); date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    disabledDates.Add(date);
            }

            return disabledDates;
        }
    }
}