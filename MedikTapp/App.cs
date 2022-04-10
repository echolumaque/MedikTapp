using MedikTapp.Services.NavigationService;
using MedikTapp.Views.MainPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//[assembly: ExportFont("Montserrat-Bold.ttf", Alias = "bold")]
//[assembly: ExportFont("Montserrat-Medium.ttf", Alias = "med")]
//[assembly: ExportFont("Montserrat-Regular.ttf", Alias = "reg")]
//[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "sembold")]
[assembly: ExportFont("Gilroy-Bold.ttf", Alias = "bold")]
[assembly: ExportFont("Gilroy-Medium.ttf", Alias = "med")]
[assembly: ExportFont("Gilroy-Regular.ttf", Alias = "reg")]
[assembly: ExportFont("Gilroy-SemiBold.ttf", Alias = "sembold")]
[assembly: ExportFont("MaterialIconsOutlined-Regular.ttf", Alias = "mato")]
[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "mat")]
[assembly: ExportFont("Font Awesome 6 Pro-Regular-400.otf", Alias = "far")]
[assembly: ExportFont("Font Awesome 6 Pro-Light-300.otf", Alias = "fal")]
[assembly: ExportFont("Font Awesome 6 Pro-Solid-900.otf", Alias = "fas")]
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MedikTapp
{
    public partial class App : Application
    {
        public App(NavigationService navigationService)
        {
            Sharpnado.Shades.Initializer.Initialize(false);
            navigationService.SetRootPage<MainPage>();
        }
    }
}