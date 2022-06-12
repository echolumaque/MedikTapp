using MedikTapp.Services.NavigationService;
using Refit;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        private void ChangeTemplate(string templateKey)
        {
            _templateKey = templateKey;
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _templateKey = "Register";
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];
        }

        private async Task Continue()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
                    ChangeTemplate("Login");
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
            else
            {
                await Application.Current.MainPage.DisplayAlert("No internet",
                    "Sorry, it seems MedikTapp's server is unavailable. Have you tried turning on your mobile data or wi-fi for internet connectivity?",
                    "Ok");
            }
        }
    }
}