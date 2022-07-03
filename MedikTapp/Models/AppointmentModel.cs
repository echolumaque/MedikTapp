using System;

namespace MedikTapp.Models
{
    public class AppointmentModel
    {
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BookingStatus { get; set; }
    }
}