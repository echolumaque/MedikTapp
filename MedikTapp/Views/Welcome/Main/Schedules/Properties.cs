using MedikTapp.Enums;
using MedikTapp.Models;
using MedikTapp.Resources.Fonts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedules
{
    public partial class SchedulesTabViewModel
    {
        public IEnumerable<BookingSort> BookingSortCollection { get; set; }
        public string BookingSortMainBoxText { get; set; }
        public IAsyncCommand<AppointmentModel> CancelScheduleCmd { get; }
        public override bool CanHaveBadge { get; set; } = true;
        public bool CanModifyAppointment { get; set; }
        public ICommand ChangeFilterCmd { get; }
        public IAsyncCommand FilterCancelledCmd { get; }
        public IAsyncCommand FilterCompletedCmd { get; }
        public IAsyncCommand FilterUpcomingCmd { get; }
        public override string Icon => FontAwesomeIcons.Clock;
        public bool IsFilterExpanded { get; set; }
        public bool IsLoading { get; set; }
        public bool IsRefreshing { get; set; }
        public ICommand OpenComboBoxCmd { get; }
        public IAsyncCommand RefreshCmd { get; }
        public IAsyncCommand<AppointmentModel> RescheduleCmd { get; }
        public ObservableCollection<AppointmentModel> Schedules { get; set; }
        public BookingSort SelectedBookingSort { get; set; }
        public BookingStatus SelectedBookingStatus { get; set; }
        public IAsyncCommand<AppointmentModel> ServiceTappedCmd { get; }
        public override string Text => "Schedules";
        public override View ViewTemplate => new SchedulesTab(this);
    }
}