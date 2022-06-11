using MedikTapp.Helpers.Command;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;

        public TimeAvailabilityPopupViewModel(NavigationService navigationService, DatabaseService databaseService) : base(navigationService)
        {
            _databaseService = databaseService;

            ChangeSelectedTimeCmd = new Command<SelectionChangedEventArgs>(args => OnSelectedTimeChanged(args));
            SelectScheduleCmd = new AsyncSingleCommand(SelectSchedule);
        }
    }
}