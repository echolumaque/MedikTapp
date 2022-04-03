using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views;
using Xamarin.Forms;

namespace MedikTapp.ViewModels
{
    public class SettingsTabViewModel : TabItemPageViewModelBase
    {
        public SettingsTabViewModel(NavigationService navigationService) : base(navigationService)
        {
        }

        public override View ViewTemplate => new SettingsTab();
        public override string Text => "Settings";
    }
}