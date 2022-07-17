using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Templates.Base
{
    public class BasePage<TViewModel> : ContentPage where TViewModel : ViewModelBase
    {
        public BasePage(in TViewModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            NavigationPage.SetHasNavigationBar(this, false);
            Visual = VisualMarker.Material;
        }

        protected TViewModel ViewModel { get; }
    }
}