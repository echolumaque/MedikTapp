using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.Bookings;
using MedikTapp.Views.Welcome.Main.Home;
using MedikTapp.Views.Welcome.Main.Schedule;
using MedikTapp.Views.Welcome.Main.Settings;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using System;
using System.Threading.Tasks;

namespace MedikTapp.Views.MainPage
{
    public class MainPageViewModel : TabMainPageViewModelBase
    {
        public MainPageViewModel(NavigationService navigationService, IServiceProvider serviceProvider,
            NotificationService notificationService)
            : base(navigationService, serviceProvider)
        {
            AddTab<HomeTabViewModel>();
            AddTab<BookingsTabViewModel>();
            AddTab<ScheduleTabViewModel>();
            AddTab<SettingsTabViewModel>();

            Task.Run(async () =>
            {
                await notificationService.Send(69420, "MedikTapp", new DateTime(2022, 5, 28, 6, 1, 0), "You have an incoming appointment!",
                    categoryType: NotificationCategoryType.Reminder,
                    androidSpecificOptions: new AndroidOptions
                    {
                        Group = "MedikTapp",
                        IsGroupSummary = true,
                        Priority = AndroidNotificationPriority.Max,
                        VisibilityType = AndroidVisibilityType.Public,
                    });
            });
        }
    }
}