using MedikTapp.Helpers.Command;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.ViewModels.Base;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.TimeAvailability
{
    public partial class TimeAvailabilityPopupViewModel : ViewModelBase
    {
        private readonly DatabaseService _databaseService;
        private readonly NotificationService _notificationService;

        public TimeAvailabilityPopupViewModel(NavigationService navigationService,
            DatabaseService databaseService,
            NotificationService notificationService)
            : base(navigationService)
        {
            _databaseService = databaseService;
            _notificationService = notificationService;

            ChangeSelectedTimeCmd = new Command<SelectionChangedEventArgs>(args => OnSelectedTimeChanged(args));
            SelectScheduleCmd = new AsyncSingleCommand(SelectSchedule, () => SelectedDate.Year != 1 && SelectedTime != null);
        }
    }
}