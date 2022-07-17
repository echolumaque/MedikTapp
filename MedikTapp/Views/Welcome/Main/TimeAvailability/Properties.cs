using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPageViewModel
    {
        public string[] CustomDayLabels => DateTimeFormatInfo.CurrentInfo.AbbreviatedDayNames;
        public List<DateTime> DisabledDates { get; set; }
        [OnChangedMethod(nameof(IsOnBehalfChanged))]
        public bool IsOnBehalf { get; set; }
        public DateTime MaximumDate => new(2022, 12, 31);
#nullable enable
        [OnChangedMethod(nameof(AppointmentFieldsChanged))]
        public string? ProspectAge { get; set; }
        [OnChangedMethod(nameof(AppointmentFieldsChanged))]
        public string? ProspectFirstName { get; set; }
        [OnChangedMethod(nameof(AppointmentFieldsChanged))]
        public string? ProspectLastName { get; set; }
        [OnChangedMethod(nameof(AppointmentFieldsChanged))]
        public string ProspectSex { get; set; }
#nullable disable
        [OnChangedMethod(nameof(SelectedDateChanged))]
        public DateTime SelectedDate { get; set; }
        [OnChangedMethod(nameof(SelectedTimeChanged))]
        public DateTime SelectedTime { get; set; }
        public IAsyncCommand SelectScheduleCmd { get; }
        public ObservableCollection<DateTime> TimeCollection { get; set; }
    }
}