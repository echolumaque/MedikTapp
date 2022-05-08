using MedikTapp.Templates.Base;

namespace MedikTapp.Views.MainPage
{
    public partial class MainPage : BasePage<MainPageViewModel>
    {
        public MainPage(MainPageViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}