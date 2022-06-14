using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
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
        private bool _isRescheduled;
        private readonly NotificationService _notificationService;
        private Models.Services _passedService;
        private readonly IToast _toast;

        public TimeAvailabilityPopupViewModel(NavigationService navigationService,
            DatabaseService databaseService,
            NotificationService notificationService,
            IToast toast)
            : base(navigationService)
        {
            _databaseService = databaseService;
            _notificationService = notificationService;
            _toast = toast;

            ChangeSelectedTimeCmd = new Command<SelectionChangedEventArgs>(args => OnSelectedTimeChanged(args));
            SelectScheduleCmd = new AsyncSingleCommand(SelectSchedule, () => SelectedDate.Year != 1 && SelectedTime != null);
        }
    }
}