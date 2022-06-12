using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.ResourceService;
using MedikTapp.Views.Onboarding;
using MedikTapp.Views.Onboarding.Account;
using Syncfusion.Licensing;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Services.InitializeDataService;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
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
        private readonly InitializeDataService _initializeDataService;
        private readonly MockService _mockService;
        private event EventHandler AppInit;

        public App(NavigationService navigationService,
            InitializeDataService initializeDataService,
            MockService mockService)
        {
            _initializeDataService = initializeDataService;
            _mockService = mockService;
            SyncfusionLicenseProvider.RegisterLicense("NjQzMTA4QDMyMzAyZTMxMmUzMGdCUTc5N2ZmN21lckRHVXp2YzdranZ2V0FGTHVKeVFSa1pVSlBCaVpWL2M9");

            DefineResources();
            VersionTracking.Track();
            AppInit += Init;
            AppInit(null, EventArgs.Empty);

            if (VersionTracking.IsFirstLaunchEver)
                navigationService.SetRootPage<OnboardingPage>();
            else
                navigationService.SetRootPage<AccountPage>();

            //navigationService.SetRootPage<MainPage>();
        }

        private void DefineResources()
        {
            Resources.Add(new LightTheme());
        }

        private async void Init()
        {
            await Task.WhenAll
            (
                _initializeDataService.Init(),
                _mockService.Init()
            ).ConfigureAwait(false);
        }

        private async void Init(object sender, EventArgs e)
        {
            await Task.WhenAll
            (
                _initializeDataService.Init(),
                _mockService.Init()
            ).ConfigureAwait(false);
        }
    }
}