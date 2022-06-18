using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTapp.Services.NotificationService
{
    public class NotificationService
    {
        private readonly IEnumerable<INotificationActionTapped> _actionTapped;
        private readonly INotificationService _notificationService;
        public NotificationService(IEnumerable<INotificationActionTapped> actionTapped, INotificationService notificationService)
        {
            _actionTapped = actionTapped;
            _notificationService = notificationService;
        }

        public void CancelByNotificationIds(params int[] notificationIdList)
            => _notificationService.Cancel(notificationIdList);

        public void CancelAll() => _notificationService.CancelAll();

        public void RegisterCategory(HashSet<NotificationCategory> categories)
        {
            _notificationService.RegisterCategoryList(categories);
        }

        public Task Send(int notificationId, string title, DateTime notificationTime, string description = null,
            string returningData = "", NotificationCategoryType categoryType = NotificationCategoryType.Reminder,
            NotificationRepeat notificationRepeat = NotificationRepeat.No, TimeSpan? notificationRepeatInterval = null,
            AndroidOptions androidSpecificOptions = null, AndroidScheduleOptions androidScheduleOptions = null)
        {
            if (notificationRepeat == NotificationRepeat.TimeInterval && notificationRepeatInterval is null)
                throw new Exception("notificationRepeatInterval should have a value if notificationRepeat value is TimeInterval");

            notificationRepeatInterval = notificationRepeat == NotificationRepeat.TimeInterval ?
                notificationRepeatInterval :
                null;

            return _notificationService.Show((notification) =>
            {
                notification.WithNotificationId(notificationId)
                .WithTitle(title)
                .WithScheduleOptions((schedule) =>
                {
                    schedule.NotifyAt(notificationTime)
                            .SetNotificationRepeatInterval(notificationRepeat, notificationRepeatInterval);

                    if (androidScheduleOptions != null)
                        schedule.WithAndroidOptions(androidScheduleOptions);

                    return schedule.Build();
                }).WithCategoryType(categoryType);

                if (!string.IsNullOrWhiteSpace(description))
                    notification.WithDescription(description);


                if (!string.IsNullOrWhiteSpace(returningData))
                    notification.WithReturningData(returningData);

                if (androidSpecificOptions != null)
                    notification.WithAndroidOptions(androidSpecificOptions);

                return notification.Create();
            });
        }

        public void ActionTapped(NotificationActionEventArgs e)
        {
            if (_actionTapped is null)
                return;

            _actionTapped.FirstOrDefault(s => s.ActionId == e.ActionId)?.Execute(e);
        }
    }
}