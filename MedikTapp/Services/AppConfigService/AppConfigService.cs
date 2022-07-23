using MedikTapp.Constants;
using MedikTapp.Tables;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials.Interfaces;

namespace MedikTapp.Services.AppConfigService
{
    public partial class AppConfigService : ObservableObject
    {
        private AppConfig _appConfig;
        private readonly DatabaseService.DatabaseService _databaseService;
        private readonly DelegateWeakEventManager _appConfigInitialized;
        private readonly NotificationService.NotificationService _notificationService;
        private readonly IPreferences _preferences;
        public event EventHandler AppConfigInitialized
        {
            add => _appConfigInitialized.AddEventHandler(value);
            remove => _appConfigInitialized.RemoveEventHandler(value);
        }

        public AppConfigService(DatabaseService.DatabaseService databaseService,
            IPreferences preferences,
            NotificationService.NotificationService notificationService)
        {
            _databaseService = databaseService;
            _notificationService = notificationService;
            _preferences = preferences;
            _appConfigInitialized = new DelegateWeakEventManager();
        }

        private async Task<AppConfig> GetConfig()
        {
            if (_appConfig == null)
                _appConfig = await _databaseService.FindSingle<AppConfig>();

            return _appConfig;
        }

        public async Task Init()
        {
            _appConfig = await GetConfig();

            if (_appConfig == null)
            {
                await _databaseService.Insert(_appConfig = new AppConfig
                {
                    Address = string.Empty,
                    Age = 0,
                    BirthDate = default,
                    ContactNumber = string.Empty,
                    Username = string.Empty,
                    FirstName = string.Empty,
                    IsBiometricLoginEnabled = false,
                    IsDarkModeEnabled = false,
                    IsPromoNotifEnabled = true,
                    LastName = string.Empty,
                    Password = string.Empty,
                    PatientId = 0,
                    Sex = "Male",
                });
            }

            var config = await _databaseService.FindSingle<AppConfig>();
            Address = config.Address;
            Age = config.Age;
            BirthDate = config.BirthDate;
            ContactNumber = config.ContactNumber;
            Username = config.Username;
            FirstName = config.FirstName;
            IsBiometricLoginEnabled = config.IsBiometricLoginEnabled;
            _preferences.Set(Preferences.IsBiometricsEnabled, IsBiometricLoginEnabled);
            IsDarkModeEnabled = config.IsDarkModeEnabled;
            IsPromoNotifEnabled = config.IsPromoNotifEnabled;
            LastName = config.LastName;
            Password = config.Password;
            PatientId = config.PatientId;
            Sex = config.Sex;
            _appConfigInitialized.RaiseEvent(this, EventArgs.Empty, nameof(AppConfigInitialized));
            _notificationService.PromoNotificationsSubscription(_appConfig.IsPromoNotifEnabled);
        }

        public async Task UpdateConfig(string propertyName, object value)
        {
            await _databaseService.Execute($"UPDATE AppConfig SET {propertyName} = ?", value).ConfigureAwait(false);

            var config = await _databaseService.FindSingle<AppConfig>().ConfigureAwait(false);
            Address = config.Address;
            Age = config.Age;
            BirthDate = config.BirthDate;
            ContactNumber = config.ContactNumber;
            Username = config.Username;
            FirstName = config.FirstName;
            IsBiometricLoginEnabled = config.IsBiometricLoginEnabled;
            _preferences.Set(Preferences.IsBiometricsEnabled, IsBiometricLoginEnabled);
            IsDarkModeEnabled = config.IsDarkModeEnabled;
            IsPromoNotifEnabled = config.IsPromoNotifEnabled;
            LastName = config.LastName;
            Password = config.Password;
            PatientId = config.PatientId;
            Sex = config.Sex;
        }
    }
}