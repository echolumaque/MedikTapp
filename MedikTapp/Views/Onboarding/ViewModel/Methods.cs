using MedikTapp.Services.NavigationService;
using System.Collections.ObjectModel;

namespace MedikTapp.Views.Onboarding
{
    public partial class OnboardingPageViewModel
    {
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Content = new ObservableCollection<onboardingModels>
            {
                new onboardingModels
                {
                    AppTitle  = "MedikTapp",
                    Description ="Book an Appointment" +
                     "\n" + "with us" ,
                    MedikTappDescription = " offers an easy appointment" +"\n"+
                     "to our best Physicians available"+"\n"+
                    "and ensuring the best quality to you",
                    //SignupCommand = new AsyncCommand(() => NavigationService.GoTo<registerViewPage>(), allowsMultipleExecutions: false),
                    //LoginCommand = new AsyncCommand(() => NavigationService.GoTo<loginViewPage>(), allowsMultipleExecutions:false),
                }
            };
        }
    }
}