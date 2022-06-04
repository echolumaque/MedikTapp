using MedikTapp.Services.NavigationService;
using Xamarin.Forms;

namespace MedikTapp.ViewModels.Base
{
    public abstract class TabItemPageViewModelBase : ViewModelBase
    {
        public TabItemPageViewModelBase(NavigationService navigationService) : base(navigationService)
        {
        }

        public bool IsCurrentTab { get; set; } = false;
        public DataTemplate Template => new(() => ViewTemplate);
        public abstract View ViewTemplate { get; }
        public abstract string Text { get; }
        public abstract string Icon { get; }
        public abstract bool CanHaveBadge { get; }
        public int BadgeCount { get; set; }
    }
}