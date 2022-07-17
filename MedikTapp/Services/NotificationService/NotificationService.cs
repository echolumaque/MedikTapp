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
        private readonly INotificationService _notificationService;
        private readonly MedikTappService.MedikTappService _medikTappService;
        private readonly IFirebasePushNotification _firebasePushNotification;

        public NotificationService(IEnumerable<INotificationActionTapped> actionTapped,
            INotificationService notificationService,
            MedikTappService.MedikTappService medikTappService,
            IFirebasePushNotification firebasePushNotification)
        {
            _actionTapped = actionTapped;
            _notificationService = notificationService;
            _medikTappService = medikTappService;
            _firebasePushNotification = firebasePushNotification;
        }

        public void ActionTapped(NotificationActionEventArgs e)
        {
            if (_actionTapped is null)
                return;

            _actionTapped.FirstOrDefault(s => s.ActionId == e.ActionId)?.Execute(e);
        }

        public void CancelByNotificationIds(params int[] notificationIdList)
        {
            _notificationService.Cancel(notificationIdList);
        }

        public void CancelAll()
        {
            _notificationService.CancelAll();
        }

        public void RegisterCategory(HashSet<NotificationCategory> categories)
        {
            _notificationService.RegisterCategoryList(categories);
        }

        public Task SendLocalNotification(int notificationId, int channelId, string title, string description, DateTime notificationTime)
        {
            return _notificationService.Show((notification) =>
            {
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
                    ChannelId = channelId.ToString(),
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
                    VisibilityType = AndroidVisibilityType.Public,
                }).Create();
            });
        }

        public void PromoNotificationsSubscription(bool isSubscribed)
        {
            if (isSubscribed)
                _firebasePushNotification.Subscribe("promo");
            else
                _firebasePushNotification.Unsubscribe("promo");
        }
    }
}