using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Onboarding.Account;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding
{
    public partial class OnboardingPageViewModel : ViewModelBase
    {
        public OnboardingPageViewModel(NavigationService navigationService) : base(navigationService)
        {
            SkipCmd = new Command(() => Position = 2);
            ContinueCmd = new Command(() => NavigationService.SetRootPage<AccountPage>(isOffWhitePageBgColor: true));
        }
    }
}