using MedikTapp.Resources.Fonts;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.MainPage.Schedule
{
    public partial class ScheduleTabViewModel
    {
        public override string Icon => FontAwesomeIcons.Clock;
        public override string Text => "Schedules";
        public override View ViewTemplate => new ScheduleTab() { BindingContext = this };
        public override bool CanHaveBadge => false;
        public ObservableCollection<Models.Bookings> Bookings { get; set; }
    }
}
