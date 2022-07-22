using MedikTapp.Helpers.Command;
using MedikTapp.Resources.Fonts;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTabViewModel
    {
        public ObservableCollection<Models.Services> AvailableServices { get; set; }
        public override bool CanHaveBadge { get; set; } = false;
        public override string Icon => FontAwesomeIcons.ClipboardMedical;
        public bool IsLoadingData { get; set; } = true;
        public bool IsRefreshing { get; set; }
        public IAsyncCommand RefreshCmd { get; }
        public ICommand SearchEntryTextChangedCmd { get; }
        public AsyncSingleCommand<Models.Services> ServiceConfirmationCmd { get; }
        public override string Text => "Services";
        public override View ViewTemplate => new ServicesTab(this);
    }
}