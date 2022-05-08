using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.Home.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        public ServiceConfirmationPopupViewModel(DatabaseService databaseService,
            NavigationService navigationService) : base(navigationService)
        {
            _databaseService = databaseService;

            AddToBookingCmd = new AsyncCommand(AddToBooking, allowsMultipleExecutions: false);
        }
    }
}