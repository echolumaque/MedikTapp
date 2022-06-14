using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.Welcome.Main.Bookings;
using MedikTapp.Views.Welcome.Main.Home;
using MedikTapp.Views.Welcome.Main.Schedule;
using MedikTapp.Views.Welcome.Main.Services;
using MedikTapp.Views.Welcome.Main.Settings;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
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

            notificationService.Send(69420, "Check out the latest promo!", DateTime.Now.AddSeconds(5),
                "Father's Day Promo\nPromo period: July 5-30, 2022",
                categoryType: NotificationCategoryType.Reminder,
                androidSpecificOptions: new()
                {
                    ChannelId = "69420",
                    Group = "MedikTapp",
                    IsGroupSummary = true,
                    IconLargeName = new AndroidIcon
                    {
                        ResourceName = "mediktapp_notif_icon",
                    },
                    IconSmallName = new AndroidIcon
                    {
                        ResourceName = "mediktapp_notif_icon",
                    },
                    Priority = AndroidNotificationPriority.Max,
                    VisibilityType = AndroidVisibilityType.Public
                });
        }
    }
}