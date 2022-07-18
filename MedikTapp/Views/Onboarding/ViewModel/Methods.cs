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
                    ImagePath = "resource://MedikTapp.Resources.Images.onboarding1.png",
                    Subtitle = "Don't forget to confirm your appointment on the Bookings tab. Then, select your preferred appointment schedule.",
                    Title = "Set your appointment"
                },
                new()
                {
                    ImagePath = "resource://MedikTapp.Resources.Images.onboarding2.png",
                    Subtitle = "See the list of your confirmed bookings. You have the option to cancel or reschedule your appointment if you wish.",
                    Title = "Always visit your schedule tab"
                },
                new()
                {
                    ImagePath = "resource://MedikTapp.Resources.Images.onboarding3.png",
                    Subtitle = "Check your notifications for your upcoming appointments and new promo services.",
                    Title = "You'll be notified"
                }
            };
        }
    }
}