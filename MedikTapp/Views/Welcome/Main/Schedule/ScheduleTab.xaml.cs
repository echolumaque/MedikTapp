using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Schedule
{
    public partial class ScheduleTab : BaseTab<ScheduleTabViewModel>
    {
        public ScheduleTab(ScheduleTabViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}