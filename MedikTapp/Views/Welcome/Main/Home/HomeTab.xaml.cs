using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTab : BaseTab<HomeTabViewModel>
    {
        public HomeTab(HomeTabViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}