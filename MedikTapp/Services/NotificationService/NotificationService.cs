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

        public NotificationService(IEnumerable<INotificationActionTapped> actionTapped)
        {
            _actionTapped = actionTapped;
        }

        public void CancelByNotificationIds(params int[] notificationIdList)
            => NotificationCenter.Current.Cancel(notificationIdList);

        public void CancelAll() => NotificationCenter.Current.CancelAll();

        public static void RegisterCategory(HashSet<NotificationCategory> categories)
        {
            NotificationCenter.Current.RegisterCategoryList(categories);
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

            return NotificationCenter.Current.Show((notification) =>
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

        //public Task Send(string description, DateTime trigger)
        //{
        //    return Send(69420, "MedikTapp", trigger, description, categoryType: NotificationCategoryType.Recommendation,
        //        androidSpecificOptions: new AndroidOptions
        //        {
        //            Group = "MedikTapp",
        //            IsGroupSummary = true,
        //            Priority = AndroidNotificationPriority.Max,
        //            VisibilityType = AndroidVisibilityType.Public,
        //        },
        //        androidScheduleOptions: new AndroidScheduleOptions
        //        {
        //            AlarmType = AndroidAlarmType.RtcWakeup,
        //            AllowedDelay = TimeSpan.FromSeconds(30)
        //        });
        //}
    }
}