using System.Windows.Input;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        public ControlTemplate AccountPageTemplate { get; set; }
        public ICommand ChangeTemplateCmd { get; }
    }
}