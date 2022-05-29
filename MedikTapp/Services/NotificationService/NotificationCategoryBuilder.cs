using Plugin.LocalNotification;
using System;
using System.Collections.Generic;

namespace MedikTapp.Services.NotificationService
{
    public class NotificationCategoryBuilder
    {
        private NotificationCategoryBuilder() { }

        public List<NotificationCategory> CategoryList { get; private set; } = new();

        public NotificationCategoryBuilder CreateCategory(NotificationCategoryType type,
            Func<NotificationCategory, HashSet<NotificationAction>> getActionList)
        {
            NotificationCategory category = new(type);
            category.ActionList = getActionList.Invoke(category);
            CategoryList.Add(category);
            return this;
        }

        public HashSet<NotificationCategory> GetCategoryList() => new(CategoryList);

        public static NotificationCategoryBuilder InitializeBuilder() => new();
    }
}