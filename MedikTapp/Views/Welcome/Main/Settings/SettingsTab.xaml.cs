using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Settings
{
    public partial class SettingsTab : BaseTab<SettingsTabViewModel>
    {
        public SettingsTab(SettingsTabViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}