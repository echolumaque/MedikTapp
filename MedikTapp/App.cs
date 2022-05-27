using MedikTapp.Services.NavigationService;
using MedikTapp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
//[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "MB")]
//[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "MM")]
//[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "MR")]
//[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "MS")]

[assembly: ExportFont("Poppins-Bold.ttf", Alias = "bold")]
[assembly: ExportFont("PoppinsMedium.ttf", Alias = "med")]
[assembly: ExportFont("PoppinsRegular.ttf", Alias = "reg")]

[assembly: ExportFont("Font Awesome 6.1.1 Pro-Light-300.otf", Alias = "fal")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Regular-400.otf", Alias = "far")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Solid-900.otf", Alias = "fas")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Thin-100.otf", Alias = "fat")]
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