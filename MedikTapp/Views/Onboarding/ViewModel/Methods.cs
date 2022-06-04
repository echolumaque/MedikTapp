using MedikTapp.Services.NavigationService;

namespace MedikTapp.Views.Onboarding
{
    public partial class OnboardingPageViewModel
    {
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Onboarding = new()
            {
                new()
                {
                    ImagePath = "resource://MedikTapp.Resources.SVGs.onboarding1.png",
                    Subtitle = "Don't forget to confirm your appointment on the Bookings tab. Remember that the schedule given is auto-generated.",
                    Title = "Set your appointment"
                },
                new()
                {
                    ImagePath = "resource://MedikTapp.Resources.SVGs.onboarding2.png",
                    Subtitle = "See the list of your confirmed bookings. You have the option to cancel or reschedule your appointment if you wish.",
                    Title = "Always visit your schedule tab"
                },
                new()
                {
                    ImagePath = "resource://MedikTapp.Resources.SVGs.onboarding3.png",
                    Subtitle = "Check your notifs to book new promo, to get ready with the approaching appointment and to be notified when your doctor is out.",
                    Title = "You'll be notified"
                }
            };
        }
    }
}