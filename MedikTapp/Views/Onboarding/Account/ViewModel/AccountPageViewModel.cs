using MedikTapp.Helpers.Command;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Refit;
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
                try
                {
                    // successful registration
                    var patientCredentials = await _httpService.Register(new()
                    {
                        { "email", Email },
                        { "password", Password },
                        { "name", Name },
                    });
                }
                catch (ApiException)
                {
                    await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Error", "Patient is already registered on MedikTapp's record", "Ok"));
                }
                _templateKey = "Login";
                ChangeTemplate();
            }
            else
            {
                try
                {
                    var patientCredentials = await _httpService.Login(new()
                    {
                        { "email", Email },
                        { "password", Password },
                    });

                    if (patientCredentials != null)
                        Device.BeginInvokeOnMainThread(() => NavigationService.SetRootPage<MainPage.MainPage>());
                    else
                        await Application.Current.MainPage.DisplayAlert("Error", "Patient is not yet registered on MedikTapp's record", "Ok");
                }
                catch (ApiException)
                {
                    await Device.InvokeOnMainThreadAsync(async () => await Application.Current.MainPage.DisplayAlert("Error", "Patient is not yet registered on MedikTapp's record", "Ok"));
                }
            }
        }
    }
}