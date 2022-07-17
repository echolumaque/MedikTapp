using MedikTapp.Resources.Fonts;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTabViewModel
    {
        public override string Icon => FontAwesomeIcons.CalendarCirclePlus;
        public override string Text => "Bookings";
        public override View ViewTemplate => new BookingsTab(this);
        public override bool CanHaveBadge { get; set; } = true;
        public ObservableRangeCollection<Models.Services> Bookings { get; set; }
        public IAsyncCommand<Models.Services> CancelBookingCmd { get; }
        public IAsyncCommand<Models.Services> ScheduleCmd { get; }
    }
}