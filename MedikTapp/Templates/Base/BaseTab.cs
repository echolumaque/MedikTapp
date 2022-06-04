using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Templates.Base
{
    public class BaseTab<TViewModel> : ContentView where TViewModel : ViewModelBase
    {
        public BaseTab(in TViewModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            BackgroundColor = Color.FromHex("#F5F5F5");
            Padding = new(20);
        }

        protected TViewModel ViewModel { get; }
    }
}