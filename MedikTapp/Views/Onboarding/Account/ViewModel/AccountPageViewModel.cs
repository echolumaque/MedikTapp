using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel : ViewModelBase
    {
        private string _templateKey;
        private readonly HttpService _httpService;
        private readonly IToast _toast;

        public AccountPageViewModel(NavigationService navigationService,
            HttpService httpService,
            IToast toast) : base(navigationService)
        {
            _httpService = httpService;
            _toast = toast;

            ContinueCmd = new AsyncSingleCommand(Continue);
            ChangeTemplateCmd = new Command<string>(ChangeTemplate);
        }
    }
}