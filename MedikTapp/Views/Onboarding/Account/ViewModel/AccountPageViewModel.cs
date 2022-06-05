using MedikTapp.Helpers.Command;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel : ViewModelBase
    {
        private string _templateKey;
        private readonly HttpService _httpService;

        public AccountPageViewModel(NavigationService navigationService, HttpService httpService) : base(navigationService)
        {
            _httpService = httpService;

            ContinueCmd = new AsyncSingleCommand(Continue);
            ChangeTemplateCmd = new Command(ChangeTemplate);
        }

        private async Task Continue()
        {
            if (_templateKey == "Register")
            {
                var test = await _httpService.Register(Name, Email, Password);
            }
            else
            {
                var test = await _httpService.Login(new()
                {
                    Email = Email,
                    Password = Password
                });
            }
        }
    }
}