using SQLite;

namespace MedikTapp.Tables
{
    public class AppConfig : DatabaseTable
    {
        [PrimaryKey, Indexed, NotNull]
        public int AppConfigId { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsBiometricLoginEnabled { get; set; }
        public bool IsDarkModeEnabled { get; set; }
        public bool IsPromoNotifEnabled { get; set; }
    }
}