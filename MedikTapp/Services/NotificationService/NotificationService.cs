using Plugin.FirebasePushNotification;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationCategoryType = Plugin.LocalNotification.NotificationCategoryType;

namespace MedikTapp.Services.NotificationService
{
    public class NotificationService
    {
        private readonly IEnumerable<INotificationActionTapped> _actionTapped;
        private readonly IFirebasePushNotification _firebasePushNotification;
        public readonly INotificationService _notificationService;

        public NotificationService(IEnumerable<INotificationActionTapped> actionTapped,
            INotificationService notificationService,
            IFirebasePushNotification firebasePushNotification)
        {
            _actionTapped = actionTapped;
            _notificationService = notificationService;
            _firebasePushNotification = firebasePushNotification;
        }

        public event NotificationTappedEventHandler NotificationTapped;

        public void ActionTapped(NotificationActionEventArgs e)
        {
            if (_actionTapped is null)
                return;
            _actionTapped.FirstOrDefault(s => s.ActionId == e.ActionId)?.Execute(e);
        }

        public void CancelAll()
        {
            _notificationService.CancelAll();
        }

        public void CancelByNotificationIds(params int[] notificationIdList)
        {
            _notificationService.Cancel(notificationIdList);
        }

        public void PromoNotificationsSubscription(bool isSubscribed)
        {
            if (isSubscribed)
                _firebasePushNotification.Subscribe("promo");
            else
                _firebasePushNotification.Unsubscribe("promo");
        }

        public void RegisterCategory(HashSet<NotificationCategory> categories)
        {
            _notificationService.RegisterCategoryList(categories);
        }

        public Task SendLocalNotification(int notificationId, string title, string description, DateTime notificationTime)
        {
            return _notificationService.Show((notification) =>
            {
                var androidIcon = new AndroidIcon { ResourceName = "mediktapp_notif_icon" };
                return notification
                .WithNotificationId(notificationId)
                .WithTitle(title)
                .WithScheduleOptions((schedule) =>
                {
                    return schedule
                    .NotifyAt(notificationTime)
                    .WithAndroidOptions(new AndroidScheduleOptions
                    {
                        AlarmType = AndroidAlarmType.RtcWakeup
                    }).Build();
                })
                .WithCategoryType(NotificationCategoryType.Recommendation)
                .WithDescription(description)
                .WithAndroidOptions(new AndroidOptions
                {
                    ChannelId = "Appointment Notifications",
                    Group = "MedikTapp",
                    IsGroupSummary = true,
                    IconLargeName = androidIcon,
                    IconSmallName = androidIcon,
                    Priority = AndroidNotificationPriority.Max,
                    VisibilityType = AndroidVisibilityType.Public,
                }).Create();
            });
        }
    }
}