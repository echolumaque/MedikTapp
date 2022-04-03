using MedikTapp.ViewModels;
using MedikTapp.Views.Base;

namespace MedikTapp.Views
{
    public partial class MainPage : BasePage<MainPageViewModel>
    {
        public MainPage(MainPageViewModel vm) : base(GetViewModel(vm))
        {
            InitializeComponent();
        }
    }
}