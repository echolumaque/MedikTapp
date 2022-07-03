using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel
    {
        public ObservableCollection<DateTime> TimeCollection { get; set; }
        [OnChangedMethod(nameof(RaiseSelectScheduleCanExecuteChanged))]
        public DateTime SelectedTime { get; set; }
        public IAsyncCommand SelectScheduleCmd { get; }
        public List<DateTime> DisabledDates { get; set; }
        [OnChangedMethod(nameof(RaiseSelectScheduleCanExecuteChanged))]
        public DateTime SelectedDate { get; set; }
        public string[] CustomDayLabels { get; set; }
        public DateTime MaxDate => new(2022, 12, 31);
    }
}