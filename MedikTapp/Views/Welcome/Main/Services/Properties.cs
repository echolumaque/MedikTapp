using MedikTapp.Helpers.Command;
using MedikTapp.Resources.Fonts;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTabViewModel
    {
        public override View ViewTemplate => new ServicesTab(this);
        public override string Text => "Services";
        public override bool CanHaveBadge => false;
        public override string Icon => FontAwesomeIcons.ClipboardMedical;
        public ObservableCollection<Models.Services> AvailableServices { get; set; }
        public ICommand SearchEntryTextChangedCmd { get; }
        public AsyncSingleCommand<Models.Services> ServiceConfirmationCmd { get; }
        public bool IsLoadingData { get; set; } = true;
    }
}