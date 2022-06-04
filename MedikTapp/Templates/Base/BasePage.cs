namespace MedikTapp.Templates.Base
{
    public class BasePage<TViewModel> : ContentPage where TViewModel : ViewModelBase
    {
        public BasePage(in TViewModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            Visual = VisualMarker.Material;
        }

        protected TViewModel ViewModel { get; }
    }
}