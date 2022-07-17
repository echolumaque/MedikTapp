using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedikTappFunctionApp.Models
{
    [Table("Patients")]
    public class PatientModel
    {
        [Key]
        public int PatientId { get; set; }

        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
    }
}