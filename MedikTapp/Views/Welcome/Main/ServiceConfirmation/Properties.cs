using System;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel
    {
        public IAsyncCommand AddToBookingCmd { get; }
        public IAsyncCommand CancelCmd { get; }
        public DateTime EarliestAvailableDate { get; set; }
        public Tuple<int, int> ProductImageSize { get; set; }
        public string ServiceDescription { get; set; }
        public ImageSource ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
    }
}