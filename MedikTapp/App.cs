using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.GraphicsService;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.Services.ResourceService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views.MainPage;
using MedikTapp.Views.Onboarding;
using MedikTapp.Views.Onboarding.Account;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Syncfusion.Licensing;
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
        private readonly AppConfigService _appConfigService;
        private readonly GraphicsService _graphicsService;
        private readonly InitializeDataService _initializeDataService;
        private readonly MedikTappService _medikTappService;
        private readonly NavigationService _navigationService;
        private readonly NotificationService _notificationService;

        public App(AppConfigService appConfigService,
            GraphicsService graphicsService,
            InitializeDataService initializeDataService,
            MedikTappService medikTappService,
            NavigationService navigationService,
            NotificationService notificationService)
        {
            _appConfigService = appConfigService;
            _graphicsService = graphicsService;
            _medikTappService = medikTappService;
            _initializeDataService = initializeDataService;
            _navigationService = navigationService;
            _notificationService = notificationService;

            SyncfusionLicenseProvider.RegisterLicense("NjQzMTA4QDMyMzAyZTMxMmUzMGdCUTc5N2ZmN21lckRHVXp2YzdranZ2V0FGTHVKeVFSa1pVSlBCaVpWL2M9");
            DefineResources();
            VersionTracking.Track();

            if (VersionTracking.IsFirstLaunchEver)
                navigationService.SetRootPage<OnboardingPage>();
            else
                navigationService.SetRootPage<AccountPage>();

            NotificationCenter.Current.NotificationTapped += GotoAppointmentsTab;
        }

        private void GotoAppointmentsTab(NotificationEventArgs e)
        {
            if (_navigationService.GetCurrentPage() is not Views.MainPage.MainPage)
                _navigationService.SetRootPage<MainPage>();

            var bindingContext = (TabMainPageViewModelBase)_navigationService.GetCurrentPage().BindingContext;
            bindingContext.ActiveTabIndex = 3;
            bindingContext.Tabs[3].IsCurrentTab = true;
            bindingContext.Tabs[3].Initialized(null);
        }

        private void DefineResources()
        {
            Resources.Add(new LightTheme());
        }

        protected override async void OnStart()
        {
            _notificationService.PromoNotificationsSubscription(true);
            await _initializeDataService.Init().ConfigureAwait(false);
            await Task.WhenAll
            (
               _medikTappService.Init(),
               _graphicsService.PreloadImages(),
               _appConfigService.Init()
            ).ConfigureAwait(false);
        }
    }
}