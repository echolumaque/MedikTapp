using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPopupViewModel
    {
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
        public string ServiceDescription { get; set; }
        public IAsyncCommand CancelCmd { get; }
        public DateTime EarliestAvailableDate { get; set; }
        public Tuple<int, int> ProductImageSize { get; set; }
        public double PopupHeight { get; set; }
        public IAsyncCommand OpenMapCmd { get; }
    }
}