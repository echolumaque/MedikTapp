using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials.Interfaces;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel : TabItemPageViewModelBase
    {
        private readonly AppConfigService _appConfigService;
        private readonly ICloseApplication _closeApplication;
        private readonly IFingerprint _fingerprint;
        private readonly HttpService _httpService;
        private readonly IMainThread _mainThread;
        private readonly MedikTappService _medikTappService;
        private readonly IToast _toast;
        private bool _isTimed;

        public HomeTabViewModel(NavigationService navigationService,
            ICloseApplication closeApplication,
            IToast toast,
            IMainThread mainThread,
            IFingerprint fingerprint,
            AppConfigService appConfigService,
            HttpService httpService,
            MedikTappService medikTappService) : base(navigationService)
        {
            _medikTappService = medikTappService;
            _closeApplication = closeApplication;
            _toast = toast;
            _httpService = httpService;
            _mainThread = mainThread;
            _appConfigService = appConfigService;
            _fingerprint = fingerprint;

            _appConfigService.AppConfigInitialized += OnAppConfigInitialized;
            _medikTappService.MedikTappServiceInitialized += OnMedikTappServiceInitialized;
            ServiceConfirmationCmd = new AsyncSingleCommand<Models.Services>(GotoServiceConfirmationPopup);
            RefreshCmd = new AsyncSingleCommand(Refresh);
        }
    }
}