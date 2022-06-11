using MedikTapp.Enums;
using MedikTapp.Resources.Fonts;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel
    {
        public override string Icon => FontAwesomeIcons.CalendarCirclePlus;
        public override string Text => "Bookings";
        public override View ViewTemplate => new BookingsTab(this);
        public override bool CanHaveBadge => true;
        public ObservableRangeCollection<Models.Services> Bookings { get; set; }
        public IAsyncCommand<Models.Services> CancelBookingCmd { get; }
        public IEnumerable<BookingSort> BookingSortCollection { get; set; }
        public BookingSort SelectedBookingSort { get; set; }
        public string BookingSortMainBoxText { get; set; }
        public ICommand OpenComboBoxCmd { get; }
        public bool IsFilterExpanded { get; set; }
        public ICommand ChangeFilterCmd { get; }
        public IAsyncCommand<Models.Services> ScheduleCmd { get; }
    }
}