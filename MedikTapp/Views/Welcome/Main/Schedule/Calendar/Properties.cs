using System;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.Schedule.Calendar
{
    public partial class CalendarPopupViewModel
    {
        public string[] CustomDayLabels { get; set; }
        public List<DateTime> DisabledDates { get; set; }
        public IAsyncCommand RescheduleCmd { get; }
        public DateTime SelectedDate { get; set; }
    }
}