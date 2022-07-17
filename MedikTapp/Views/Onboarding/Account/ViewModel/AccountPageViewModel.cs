using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel : ViewModelBase
    {
        private readonly AppConfigService _appConfigService;
        private readonly IDeviceDisplay _deviceDisplay;
        private readonly IFingerprint _fingerprint;
        private readonly HttpService _httpService;
        private readonly IMainThread _mainThread;
        private readonly IToast _toast;
        private readonly IConnectivity _connectivity;

        public AccountPageViewModel(NavigationService navigationService,
            IMainThread mainThread,
            IConnectivity connectivity,
            IFingerprint fingerprint,
            HttpService httpService,
            AppConfigService appConfigService,
            IToast toast,
            IDeviceDisplay deviceDisplay) : base(navigationService)
        {
            _appConfigService = appConfigService;
            _connectivity = connectivity;
            _fingerprint = fingerprint;
            _httpService = httpService;
            _toast = toast;
            _mainThread = mainThread;
            _deviceDisplay = deviceDisplay;
            _appConfigService.AppConfigInitialized += AppConfigInitialized;

            BiometricsCmd = new AsyncSingleCommand(BiometricsLogin, () => IsBiometricsAvailable);
            LoginCmd = new AsyncSingleCommand(() => Login(false), () => AllLoginValidations());
            ChangeTemplateCmd = new Command<string>(key => AccountPageTemplate = key);
            RegisterCmd = new AsyncSingleCommand(Register, () => AllRegistrationValidatons());
            JoinUsCmd = new AsyncSingleCommand<ScrollView>(JoinUs);
        }
    }
}