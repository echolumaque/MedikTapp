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
        private readonly IFingerprint _fingerprint;
        private readonly HttpService _httpService;
        private string _templateKey;
        private readonly IToast _toast;
        private readonly AppConfigService _appConfigService;
        private readonly IMainThread _mainThread;

        public AccountPageViewModel(NavigationService navigationService,
            IMainThread mainThread,
            IFingerprint fingerprint,
            HttpService httpService,
            AppConfigService appConfigService,
            IToast toast) : base(navigationService)
        {
            _appConfigService = appConfigService;
            _fingerprint = fingerprint;
            _httpService = httpService;
            _toast = toast;
            _mainThread = mainThread;

            _appConfigService.AppConfigInitialized += AppConfigInitialized;
            BiometricsCmd = new AsyncSingleCommand(BiometricsLogin, () => IsBiometricsAvailable);
            ContinueCmd = new AsyncSingleCommand(() => Continue(false), () => CanContinue);
            ChangeTemplateCmd = new Command<string>(ChangeTemplate);
        }
    }
}