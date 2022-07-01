using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
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
        private readonly IPreferences _preferences;

        public AccountPageViewModel(NavigationService navigationService,
            IFingerprint fingerprint,
            HttpService httpService,
            IPreferences preferences,
            IToast toast) : base(navigationService)
        {
            _fingerprint = fingerprint;
            _httpService = httpService;
            _toast = toast;
            _preferences = preferences;

            BiometricsCmd = new AsyncSingleCommand(BiometricsLogin, () => IsBiometricsAvailable);
            ContinueCmd = new AsyncSingleCommand(Continue);
            ChangeTemplateCmd = new Command<string>(ChangeTemplate);
        }
    }
}