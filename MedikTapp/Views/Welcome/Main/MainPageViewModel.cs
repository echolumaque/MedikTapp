using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.Bookings;
using MedikTapp.Views.Welcome.Main.Home;
using MedikTapp.Views.Welcome.Main.Schedule;
using MedikTapp.Views.Welcome.Main.Services;
using MedikTapp.Views.Welcome.Main.Settings;
using System;

namespace MedikTapp.Views.MainPage
{
    public class MainPageViewModel : TabMainPageViewModelBase
    {
        public MainPageViewModel(NavigationService navigationService, IServiceProvider serviceProvider,
            NotificationService notificationService)
            : base(navigationService, serviceProvider)
        {
            AddTab<HomeTabViewModel>();
            AddTab<ServicesTabViewModel>();
            AddTab<BookingsTabViewModel>();
            AddTab<ScheduleTabViewModel>();
            AddTab<SettingsTabViewModel>();

            notificationService.Send("You have an incoming appointment!\nChest X-Ray\nJune 12,2022 | 3:00PM",
                DateTime.Now.AddSeconds(5));
        }
    }
}