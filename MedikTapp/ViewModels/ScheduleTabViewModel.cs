using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views;
using Xamarin.Forms;

namespace MedikTapp.ViewModels
{
    public class ScheduleTabViewModel : TabItemPageViewModelBase
    {
        public ScheduleTabViewModel(NavigationService navigationService) : base(navigationService)
        {
        }

        public override View ViewTemplate => new ScheduleTab();
        public override string Text => "Schedule";
    }
}