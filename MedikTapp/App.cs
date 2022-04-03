using MedikTapp.Services.NavigationService;
using MedikTapp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MedikTapp
{
    public partial class App : Application
    {
        public App(NavigationService navigationService)
        {
            navigationService.SetRootPage<MainPage>();
        }
    }
}