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
        public DateTime AppointmentDate { get; set; }
        public string BookingStatus { get; set; }
        public bool InBehalf { get; set; }
#nullable enable
        public string? ProspectFirstName { get; set; }
        public string? ProspectLastName { get; set; }
        public int? ProspectAge { get; set; }
        public string? ProspectGender { get; set; }
#nullable disable

        // Foreign entities (navigation properties)
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("PatientId")]
        public PatientModel Patients { get; set; }
        [ForeignKey("ServiceId")]
        public ServiceModel Services { get; set; }
    }
}