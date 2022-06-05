using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedikTappFunctionApp.Models
{
    [Table("Patient")]
    public class PatientModel
    {
        [Key]
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}