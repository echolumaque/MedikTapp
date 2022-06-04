using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel : ViewModelBase
    {
        private string _templateKey;

        public AccountPageViewModel(NavigationService navigationService) : base(navigationService)
        {
            ContinueCmd = new Command(() => NavigationService.SetRootPage<MainPage.MainPage>());
            ChangeTemplateCmd = new Command(ChangeTemplate);
        }
    }
}