using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;

namespace MedikTapp.Views.MainPage.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        public HomeTabViewModel(NavigationService navigationService) : base(navigationService)
        {
        }
    }
}