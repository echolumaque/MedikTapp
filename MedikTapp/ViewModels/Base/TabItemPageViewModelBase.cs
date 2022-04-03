using MedikTapp.Services.NavigationService;
using Xamarin.Forms;

namespace MedikTapp.ViewModels.Base
{
    public abstract class TabItemPageViewModelBase : ViewModelBase
    {
        public TabItemPageViewModelBase(NavigationService navigationService) : base(navigationService)
        {
        }

        public virtual string Icon { get; } = "";
        public bool IsCurrentTab { get; set; } = false;
        public DataTemplate Template => new(() => ViewTemplate);
        public abstract View ViewTemplate { get; }
        public abstract string Text { get; }
    }
}