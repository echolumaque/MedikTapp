using System;

namespace MedikTapp.Models
{
    public class AddAppointmentModel
    {
        // TODO: Used to add appointment
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BookingStatus { get; set; }
        public bool InBehalf { get; set; }
#nullable enable
        public string? ProspectFirstName { get; set; }
        public string? ProspectLastName { get; set; }
        public int? ProspectAge { get; set; }
        public string? ProspectGender { get; set; }
#nullable disable
    }
}