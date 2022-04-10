using MedikTapp.ViewModels;
using MedikTapp.Views.Base;

namespace MedikTapp.Views
{

    public partial class onboardingViewPage : BasePage<onboardingViewModel>
    {
        public onboardingViewPage(onboardingViewModel vm) : base(GetViewModel(vm))
        {
            InitializeComponent();
        }
    }
}