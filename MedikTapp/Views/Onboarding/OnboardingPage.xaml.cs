using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Onboarding
{
    public partial class OnboardingPage : BasePage<OnboardingPageViewModel>
    {
        public OnboardingPage(OnboardingPageViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}