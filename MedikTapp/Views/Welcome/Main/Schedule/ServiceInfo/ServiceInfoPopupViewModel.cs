using MedikTapp.Helpers.Command;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPopupViewModel : ViewModelBase
    {
        public ServiceInfoPopupViewModel(NavigationService navigationService) : base(navigationService)
        {
            OpenMapCmd = new AsyncSingleCommand(OpenMap);
            PopupHeight = Application.Current.MainPage.Height / 1.2;
        }
    }
}