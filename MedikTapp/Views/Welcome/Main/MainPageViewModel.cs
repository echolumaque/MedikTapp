using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.Bookings;
using MedikTapp.Views.Welcome.Main.Home;
using MedikTapp.Views.Welcome.Main.Schedules;
using MedikTapp.Views.Welcome.Main.Services;
using MedikTapp.Views.Welcome.Main.Settings;
using System;

namespace MedikTapp.Views.MainPage
{
    public class MainPageViewModel : TabMainPageViewModelBase
    {
        public MainPageViewModel(NavigationService navigationService,
            IServiceProvider serviceProvider,
            NotificationService notificationService)
            : base(navigationService, serviceProvider)
        {
            AddTab<HomeTabViewModel>();
            AddTab<ServicesTabViewModel>();
            AddTab<BookingsTabViewModel>();
            AddTab<SchedulesTabViewModel>();
            AddTab<SettingsTabViewModel>();
        }
    }
}