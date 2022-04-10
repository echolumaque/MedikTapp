using MedikTapp.Models;
using MedikTapp.Resources.Fonts;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.MainPage.Home
{
    public partial class HomeTabViewModel
    {
        public override string Icon => FontAwesomeIcons.HouseBlank;
        public override string Text => "Home";
        public override View ViewTemplate => new HomeTab() { BindingContext = this };
        public override bool CanHaveBadge => false;
        public ObservableCollection<Promos> PromosCollection { get; set; }
        public ObservableCollection<ServicesOffered> ServicesCollection { get; set; }
    }
}