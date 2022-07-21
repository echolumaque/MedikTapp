using System;

namespace MedikTapp.Models
{
    public class AppointmentModel
    {
        // TODO: Used in Appointments tab (former schedule tab)
        public DateTime AppointmentDate { get; set; }
        public int AppointmentId { get; set; }
        public string BookingStatus { get; set; }
#nullable enable
        public string? ProspectFirstName { get; set; }
        public string? ProspectLastName { get; set; }
        public string? ProspectGender { get; set; }
        public int? ProspectAge { get; set; }
#nullable disable
        public string ServiceDescription { get; set; }
        public int ServiceId { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
    }
}