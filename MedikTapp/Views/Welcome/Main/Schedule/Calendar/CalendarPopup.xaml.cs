using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Schedule.Calendar
{
    public partial class CalendarPopup : BasePopup<CalendarPopupViewModel>
    {
        public CalendarPopup(CalendarPopupViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}