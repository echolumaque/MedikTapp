using MedikTapp.Models;
using MedikTapp.Services.NavigationService;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPageViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            //todo here
            var passedAppointment = parameters.GetValue<AppointmentModel>("appointment");
            AppointmentDate = passedAppointment.AppointmentDate;
            ServiceImage = passedAppointment.ServiceImage;
            ServiceName = passedAppointment.ServiceName;
            ServiceDescription = passedAppointment.ServiceDescription;
            ServicePrice = passedAppointment.ServicePrice;

            HasProspect = !string.IsNullOrWhiteSpace(passedAppointment.ProspectFirstName)
                && !string.IsNullOrWhiteSpace(passedAppointment.ProspectLastName)
                && !string.IsNullOrWhiteSpace(passedAppointment.ProspectGender)
                && passedAppointment.ProspectAge != null;
            if (!HasProspect)
                return;
            ProspectAge = passedAppointment.ProspectAge;
            ProspectFirstName = passedAppointment.ProspectFirstName;
            ProspectLastName = passedAppointment.ProspectLastName;
            ProspectSex = passedAppointment.ProspectGender;

            //ProductImageSize = passedService.IsPromo
            //    ? Tuple.Create(200, 250)
            //    : Tuple.Create(100, 100);
        }

        private Task OpenMap()
        {
            var pudcAddress = "Casquejo, 2150 Airport Rd, Baclaran, Parañaque, 1700 Metro Manila";
            IsLoadingLocation = true;
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));
            return _geolocation.GetLocationAsync(request)
                .ContinueWith(async location =>
                {
                    try
                    {
                        var position = new Position(location.Result.Latitude, location.Result.Longitude);
                        var possibleAddress = await new Geocoder().GetAddressesForPositionAsync(position);
                        var address = possibleAddress.FirstOrDefault();

                        await _launcher.OpenAsync($"http://maps.google.com/?daddr={Uri.EscapeDataString(pudcAddress)}&saddr={Uri.EscapeDataString(address)}");
                        IsLoadingLocation = false;
                    }
                    catch (Exception)
                    {
                        await _launcher.OpenAsync($"geo:0,0?q={Uri.EscapeDataString(pudcAddress)}");
                    }

                    IsLoadingLocation = false;
                });
        }
    }
}