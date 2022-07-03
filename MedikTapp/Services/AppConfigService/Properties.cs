namespace MedikTapp.Services.AppConfigService
{
    public partial class AppConfigService
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsBiometricLoginEnabled { get; set; }
        public bool IsDarkModeEnabled { get; set; }
        public bool IsPromoNotifEnabled { get; set; }
    }
}