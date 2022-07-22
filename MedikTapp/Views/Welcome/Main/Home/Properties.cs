using MedikTapp.Resources.Fonts;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel
    {
        public override bool CanHaveBadge { get; set; } = false;
        public override string Icon => FontAwesomeIcons.House;
        public bool IsLoadingData { get; set; } = true;
        public bool IsRefreshing { get; set; }
        public int PromoPosition { get; set; }
        public ObservableCollection<Models.Services> PromosCollection { get; set; }
        public IAsyncCommand RefreshCmd { get; }
        public IAsyncCommand<Models.Services> ServiceConfirmationCmd { get; }
        public ObservableCollection<Models.Services> ServicesCollection { get; set; }
        public override string Text => "Home";
        public override View ViewTemplate => new HomeTab(this);
    }
}