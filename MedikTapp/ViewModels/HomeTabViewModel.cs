using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views;
using Xamarin.Forms;

namespace MedikTapp.ViewModels
{
    public class HomeTabViewModel : TabItemPageViewModelBase
    {
        public HomeTabViewModel(NavigationService navigationService) : base(navigationService)
        {
        }

        public override View ViewTemplate => new HomeTab();
        public override string Text => "Home";
    }
}