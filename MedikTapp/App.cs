using MedikTapp.Services.NavigationService;
using MedikTapp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "MB")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "MM")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "MR")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "MS")]
namespace MedikTapp
{
    public partial class App : Application
    {
        public App(NavigationService navigationService)
        {
            navigationService.SetRootPage<onboardingViewPage>();
        }
    }
}