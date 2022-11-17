using MedikTapp.Interfaces;
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
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Services.InitializeDataService;
using Preferences = MedikTapp.Constants.Preferences;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
[assembly: ExportFont("Poppins-Bold.ttf", Alias = "bold")]
[assembly: ExportFont("PoppinsMedium.ttf", Alias = "med")]
[assembly: ExportFont("PoppinsRegular.ttf", Alias = "reg")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Light-300.otf", Alias = "fal")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Regular-400.otf", Alias = "far")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Solid-900.otf", Alias = "fas")]
[assembly: ExportFont("Font Awesome 6.1.1 Pro-Thin-100.otf", Alias = "fat")]

namespace MedikTapp;

public partial class App : Application
{
    private readonly AppConfigService _appConfigService;
    private readonly IConnectivity _connectivity;
    private readonly GraphicsService _graphicsService;
    private readonly InitializeDataService _initializeDataService;
    private readonly IMainThread _mainThread;
    private readonly MedikTappService _medikTappService;
    private readonly NavigationService _navigationService;
    private readonly NotificationService _notificationService;
    private readonly IToast _toast;

    public App(AppConfigService appConfigService,
        IConnectivity connectivity,
        GraphicsService graphicsService,
        InitializeDataService initializeDataService,
        IMainThread mainThread,
        MedikTappService medikTappService,
        NavigationService navigationService,
        NotificationService notificationService,
        IToast toast,
        IPreferences preferences)
    {
        _appConfigService = appConfigService;
        _connectivity = connectivity;
        _graphicsService = graphicsService;
        _medikTappService = medikTappService;
        _initializeDataService = initializeDataService;
        _mainThread = mainThread;
        _navigationService = navigationService;
        _notificationService = notificationService;
        _toast = toast;

        SyncfusionLicenseProvider.RegisterLicense("NjQzMTA4QDMyMzAyZTMxMmUzMGdCUTc5N2ZmN21lckRHVXp2YzdranZ2V0FGTHVKeVFSa1pVSlBCaVpWL2M9");
        Resources.Add(new LightTheme());
        VersionTracking.Track();

        if (VersionTracking.IsFirstLaunchEver)
            navigationService.SetRootPage<OnboardingPage>();
        else
        {
            if (preferences.Get(Preferences.HasLoggedAccount, false))
                navigationService.SetRootPage<MainPage>(isOffWhitePageBgColor: true);
            else
                navigationService.SetRootPage<AccountPage>(isOffWhitePageBgColor: true);
        }

        NotificationCenter.Current.NotificationTapped += GotoAppointmentsTab;
    }

    private void GotoAppointmentsTab(NotificationEventArgs e)
    {
        if (_connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            _navigationService.SetRootPage<MainPage>(isOffWhitePageBgColor: true);

            var bindingContext = (TabMainPageViewModelBase)_navigationService.GetCurrentPage().BindingContext;
            bindingContext.ActiveTabIndex = 3;
            foreach (var item in bindingContext.Tabs)
                item.IsCurrentTab = false;

            bindingContext.Tabs[3].IsCurrentTab = true;
            bindingContext.Tabs[3].Initialized(null);
        }
    }

    protected override async void OnStart()
    {
        await _mainThread.InvokeOnMainThreadAsync(() => MainPage.DisplayAlert("Created by the MedikTapp Team",
            "Capstone Symposium Best Presentor\n\n" +
            "With the supervision of Prof. Josephine M. Dela Isla under DICT 3-1 Batch 2021-2022\n\n" +
            "Group Leader:\n" +
            "Recreo, Ann Bernadette B.\n\n" +
            "Developers:\n" +
            "Lumaque, Jhericoh Janquill N. - Lead Developer\n" +
            "Corpuz, Bithiah Pelaiah T.\n\n" +
            "Group Members:\n" +
            "Guisadio, Ana Beatriz A.\n" +
            "Hernandez, Johannah Marie R.\n" +
            "Corales, Mark Reinell S.\n" +
            "Fernandez, Kathrina Bianca R.\n" +
            "Serrano, Regina Lei C.",
            "OK"));

        if (_connectivity.NetworkAccess == NetworkAccess.Internet)
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
        else
        {
            _toast.Show("An active internet connection is required to use MedikTapp.");
        }

        //await _mainThread.InvokeOnMainThreadAsync(() => MainPage.DisplayAlert("Created by the MedikTapp Team",
        //    "Capstone Symposium Best Presentor\n\n" +
        //    "With the supervision of Prof. Josephine M. Dela Isla under DICT 3-1 Batch 2021-2022\n\n" +
        //    "Group Leader:\n" +
        //    "Recreo, Ann Bernadette B.\n\n" +
        //    "Developers:\n" +
        //    "Lumaque, Jhericoh Janquill N. - Lead Developer\n" +
        //    "Corpuz, Bithiah Pelaiah T.\n\n" +
        //    "Group Members:\n" +
        //    "Guisadio, Ana Beatriz A.\n" +
        //    "Hernandez, Johannah Marie R.\n" +
        //    "Corales, Mark Reinell S.\n" +
        //    "Fernandez, Kathrina Bianca R.\n" +
        //    "Serrano, Regina Lei C.",
        //    "OK"));
    }
}