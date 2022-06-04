using Plugin.LocalNotification;
using System.Collections.Generic;

namespace MedikTapp.Services.NotificationService
{
    public class NotificationCategoryActionBuilder
    {
        private readonly HashSet<NotificationAction> _actionList = new();

        private NotificationCategoryActionBuilder() { }

        public static NotificationCategoryActionBuilder InitilizeBuilder(NotificationCategory category) => new();

        public NotificationCategoryActionBuilder AddAction(NotificationAction action)
        {
            _actionList.Add(action);
            return this;
        }

        public HashSet<NotificationAction> GetActionList() => _actionList;
    }
}