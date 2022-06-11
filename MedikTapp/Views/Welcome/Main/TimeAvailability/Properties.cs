using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel
    {
        public ObservableCollection<Models.TimeAvailability> TimeCollection { get; set; }
        public ICommand ChangeSelectedTimeCmd { get; }
        public Models.TimeAvailability SelectedTime { get; set; }
        public IAsyncCommand SelectScheduleCmd { get; }
        public List<DateTime> DisabledDates { get; set; }
        public DateTime SelectedDate { get; set; }
        public string[] CustomDayLabels { get; set; }
    }
}