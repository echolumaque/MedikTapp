using MedikTapp.Services.NavigationService;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        private void ChangeTemplate()
        {
            _templateKey = _templateKey == "Register" ? "Login" : "Register";
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            _templateKey = "Register";
            AccountPageTemplate = (ControlTemplate)NavigationService.GetCurrentPage().Resources[_templateKey];
        }
    }
}