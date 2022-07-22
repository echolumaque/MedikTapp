using Android.App;
using Android.Content;
using Android.Graphics;
using AndroidX.Core.App;
using Firebase.Messaging;

namespace MedikTapp.Droid
{
    [Service(Exported = true, DirectBootAware = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MedikTappFirebaseService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(int.Parse(message.Data["promoNotifId"]), new NotificationCompat.Builder(Application.Context, "Promo Notifications")
                .SetColor(Color.ParseColor("#695CD5"))
                .SetGroup("MedikTapp")
                .SetGroupSummary(true)
                .SetAutoCancel(true)
                .SetContentTitle(message.Data["title"])
                .SetContentText(message.Data["body"])
                .SetStyle(new NotificationCompat.BigTextStyle().BigText(message.Data["body"]))
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.mediktapp_notif_icon))
                .SetPriority((int)NotificationPriority.Max)
                .SetVisibility((int)NotificationVisibility.Public)
                .SetSmallIcon(Resource.Drawable.mediktapp_notif_icon).Build());
        }
    }
}