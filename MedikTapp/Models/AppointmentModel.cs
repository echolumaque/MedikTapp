using System;

namespace MedikTapp.Models
{
    public class AppointmentModel
    {
        // TODO: Used in Appointments tab (former schedule tab)
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceImage { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BookingStatus { get; set; }
#nullable enable
        public string? ProspectFirstName { get; set; }
        public string? ProspectLastName { get; set; }
#nullable disable
    }
}