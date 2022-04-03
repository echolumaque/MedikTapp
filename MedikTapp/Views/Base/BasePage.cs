using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Base
{
    public abstract class BasePage<TViewModel> : ContentPage where TViewModel : ViewModelBase
    {
        protected BasePage(in TViewModel viewModel) => BindingContext = ViewModel = viewModel;

        protected TViewModel ViewModel { get; }

        protected static TViewModel GetViewModel(TViewModel viewModel) => viewModel;
    }
}