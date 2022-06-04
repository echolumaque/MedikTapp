using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.Schedule.Calendar
{
    public partial class CalendarPopupViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            var parameter = parameters.GetValue<Schedules>("schedule");
            _passedSchedule = parameter;
            CustomDayLabels = DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
            DisabledDates = GetDisabledDates();
            SelectedDate = parameter.Schedule;
            //SelectedDate = new DateTime(2022, 5, 26);
        }

        private async Task Reschedule()
        {
            await _databaseService.Update(new Schedules
            {
                ScheduleId = _passedSchedule.ScheduleId,
                BookingStatus = _passedSchedule.BookingStatus,
                ProductDescription = _passedSchedule.ProductDescription,
                ProductImagePath = _passedSchedule.ProductImagePath,
                ProductName = _passedSchedule.ProductName,
                ProductPrice = _passedSchedule.ProductPrice,
                Schedule = SelectedDate
            });
            await NavigationService.PopPopup();
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