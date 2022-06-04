using Plugin.LocalNotification.EventArgs;

namespace MedikTapp.Services.NotificationService
{
    public interface INotificationActionTapped
    {
        int ActionId { get; }
        void Execute(NotificationActionEventArgs e);
    }
}