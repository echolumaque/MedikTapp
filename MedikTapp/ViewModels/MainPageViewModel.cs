using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using System;

namespace MedikTapp.ViewModels
{
    public class MainPageViewModel : TabMainPageViewModelBase
    {
        public MainPageViewModel(NavigationService navigationService, IServiceProvider serviceProvider)
            : base(navigationService, serviceProvider)
        {
            AddTab<HomeTabViewModel>();
            AddTab<CartTabViewModel>();
            AddTab<ScheduleTabViewModel>();
            AddTab<SettingsTabViewModel>();
        }
    }
}