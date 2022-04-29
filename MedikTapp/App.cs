using MedikTapp.Services.NavigationService;
using MedikTapp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "MB")]
[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "MM")]
[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "MR")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "MS")]

[assembly: ExportFont("Gilroy-Bold.ttf", Alias = "GB")]
[assembly: ExportFont("Gilroy-Medium.ttf", Alias = "GM")]
[assembly: ExportFont("Gilroy-Regular.ttf", Alias = "GR")]
[assembly: ExportFont("Gilroy-SemiBold.ttf", Alias = "GS")]
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