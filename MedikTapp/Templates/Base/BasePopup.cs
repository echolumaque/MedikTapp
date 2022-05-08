using MedikTapp.ViewModels.Base;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace MedikTapp.Templates.Base
{
    public class BasePopup<TViewModel> : PopupPage where TViewModel : ViewModelBase
    {
        protected BasePopup(in TViewModel viewModel)
        {
            BindingContext = ViewModel = viewModel;
            Animation = new FadeAnimation
            {
                DurationIn = 500,
                DurationOut = 500,
                EasingIn = Easing.CubicInOut,
                EasingOut = Easing.CubicInOut,
                HasBackgroundAnimation = true
            };

            CloseWhenBackgroundIsClicked = true;
            HasKeyboardOffset = true;
            HasSystemPadding = true;
        }

        protected TViewModel ViewModel { get; }
    }
}