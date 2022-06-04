using MedikTapp.Resources.Fonts;
using System.Collections.ObjectModel;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel
    {
        public override string Icon => FontAwesomeIcons.HouseBlank;
        public override string Text => "Home";
        public override View ViewTemplate => new HomeTab(this);
        public override bool CanHaveBadge => false;
        public ObservableCollection<Models.Services> Promos { get; set; }
        public ObservableCollection<Models.Services> ServicesCollection { get; set; }
        public IAsyncCommand<Models.Services> ServiceConfirmationCmd { get; }
        public IAsyncCommand GotoProductsCmd { get; }
        public int PromoPosition { get; set; }
    }
}