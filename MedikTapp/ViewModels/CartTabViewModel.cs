using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using MedikTapp.Views;
using Xamarin.Forms;

namespace MedikTapp.ViewModels
{
    public class CartTabViewModel : TabItemPageViewModelBase
    {
        public CartTabViewModel(NavigationService navigationService) : base(navigationService)
        {
        }

        public override View ViewTemplate => new CartTab();
        public override string Text => "Cart";
    }
}