using MedikTapp.Helpers.Command;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private string _base64String;
        public ServiceConfirmationPopupViewModel(DatabaseService databaseService,
            NavigationService navigationService) : base(navigationService)
        {
            _databaseService = databaseService;

            AddToBookingCmd = new AsyncSingleCommand(AddToBooking);
        }
    }
}