using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MedikTapp.Views.Onboarding
{
    public partial class OnboardingPageViewModel
    {
        public ObservableCollection<OnboardingModel> Onboarding { get; set; }
        public int Position { get; set; }
        public ICommand SkipCmd { get; }
        public ICommand ContinueCmd { get; }
        public bool IsSkipVisible => Position < 2;
    }
}