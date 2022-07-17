using System;

namespace MedikTapp.Services.AppConfigService
{
    public partial class AppConfigService
    {
        public string Address { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public bool IsBiometricLoginEnabled { get; set; }
        public bool IsDarkModeEnabled { get; set; }
        public bool IsPromoNotifEnabled { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int PatientId { get; set; }
        public string Sex { get; set; }
    }
}