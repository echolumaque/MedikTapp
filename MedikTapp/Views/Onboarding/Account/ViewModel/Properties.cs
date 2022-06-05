using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        public ControlTemplate AccountPageTemplate { get; set; }
        public IAsyncCommand ContinueCmd { get; }
        public ICommand ChangeTemplateCmd { get; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}