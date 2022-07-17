using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Schedules
{
    public partial class SchedulesTab : BaseTab<SchedulesTabViewModel>
    {
        public SchedulesTab(SchedulesTabViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}