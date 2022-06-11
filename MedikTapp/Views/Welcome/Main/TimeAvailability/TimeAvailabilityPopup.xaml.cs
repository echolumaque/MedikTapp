using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopup : BasePopup<TimeAvailabilityPopupViewModel>
    {
        public TimeAvailabilityPopup(TimeAvailabilityPopupViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}