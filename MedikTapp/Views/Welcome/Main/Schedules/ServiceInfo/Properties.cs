using System;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPageViewModel
    {
        public DateTime AppointmentDate { get; set; }
        public IAsyncCommand CancelCmd { get; }
        public bool IsLoadingLocation { get; set; }
        public IAsyncCommand OpenMapCmd { get; }
        public Tuple<int, int> ProductImageSize { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
#nullable enable
        public string? ProspectFirstName { get; set; }
        public string? ProspectLastName { get; set; }
        public string? ProspectSex { get; set; }
        public int? ProspectAge { get; set; }
#nullable disable
        public bool HasProspect { get; set; }
    }
}