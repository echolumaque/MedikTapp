using MedikTapp.Services.NavigationService;
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

        public override void Initialized(NavigationParameters parameters)
        {
            _passedService = parameters.GetValue<Models.Services>("booking");
            CustomDayLabels = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
            DisabledDates = GetDisabledDates();
            SelectedDate = _passedService.AvailableTime;

            TimeCollection = new();
            for (var index = 0; index < 24; index++)
            {
                TimeCollection.Add(new()
                {
                    IsAvailable = index % 2 == 0,
                    Time = DateTime.Today.Add(new TimeSpan(index, 0, 0))
                });
            }
        }

        private async Task SelectSchedule()
        {
            await _databaseService.Update(new Models.Services
            {
                AvailableTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Time.Hour, SelectedTime.Time.Minute, SelectedTime.Time.Second),
                BookingStatus = Enums.BookingStatus.Confirmed,
                ServiceDescription = _passedService.ServiceDescription,
                ServiceId = _passedService.ServiceId,
                ServiceImagePath = _passedService.ServiceImagePath,
                ServiceName = _passedService.ServiceName,
                ServicePrice = _passedService.ServicePrice
            });
            await NavigationService.PopPopup();
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
            var startDate = new DateTime(2022, DateTime.Now.Month, DateTime.Now.Day);
            for (var index = 0; index < 3; index++)
            {
                disabledDates.Add(startDate.AddDays(index + 1));
            }

            return disabledDates;
        }
    }
}