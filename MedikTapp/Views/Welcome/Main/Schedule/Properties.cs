using MedikTapp.Enums;
using MedikTapp.Models;
using MedikTapp.Resources.Fonts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTabViewModel
    {
        public override string Icon => FontAwesomeIcons.Clock;
        public override string Text => "Schedules";
        public override View ViewTemplate => new ScheduleTab(this);
        public override bool CanHaveBadge => false;
        public ObservableCollection<Schedules> Schedules { get; set; }
        public BookingStatus SelectedBookingStatus { get; set; }
        public ICommand FilterUpcomingCmd { get; }
        public ICommand FilterCompletedCmd { get; }
        public ICommand FilterCancelledCmd { get; }
        public IEnumerable<BookingSort> BookingSortCollection { get; set; }
        public BookingSort SelectedBookingSort { get; set; }
        public string BookingSortMainBoxText { get; set; }
        public ICommand OpenComboBoxCmd { get; }
        public bool IsFilterExpanded { get; set; }
        public ICommand ChangeFilterCmd { get; }
        public IAsyncCommand<Schedules> CancelScheduleCmd { get; }
        public IAsyncCommand<Schedules> RescheduleCmd { get; }
    }
}