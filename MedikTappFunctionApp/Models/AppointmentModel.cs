using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedikTappFunctionApp.Models
{
    [Table("Appointments")]
    public class AppointmentModel
    {
        [Key]
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BookingStatus { get; set; }
    }
}