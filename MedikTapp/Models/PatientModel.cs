using System;

namespace MedikTapp.Models
{
    public class PatientModel
    {
        public int PatientId { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
    }
}