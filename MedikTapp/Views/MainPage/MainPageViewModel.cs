using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.MainPage.Bookings;
using MedikTapp.Views.MainPage.Home;
using MedikTapp.Views.MainPage.Schedule;
using MedikTapp.Views.MainPage.Settings;
using System;

namespace MedikTapp.Views.MainPage
{
    public class MainPageViewModel : TabMainPageViewModelBase
    {
        public MainPageViewModel(NavigationService navigationService, IServiceProvider serviceProvider)
            : base(navigationService, serviceProvider)
        {
            AddTab<HomeTabViewModel>();
            AddTab<BookingsTabViewModel>();
            AddTab<ScheduleTabViewModel>();
            AddTab<SettingsTabViewModel>();
        }
    }
}