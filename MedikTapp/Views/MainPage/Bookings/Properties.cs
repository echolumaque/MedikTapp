using MedikTapp.Resources.Fonts;
using Xamarin.Forms;

namespace MedikTapp.Views.MainPage.Bookings
{
    public partial class BookingsTabViewModel
    {
        public override string Icon => FontAwesomeIcons.CalendarCirclePlus;
        public override string Text => "Bookings";
        public override View ViewTemplate => new BookingsTab() { BindingContext = this };
        public override bool CanHaveBadge => true;

    }
}