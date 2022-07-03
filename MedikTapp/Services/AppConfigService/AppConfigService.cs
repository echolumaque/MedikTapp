using MedikTapp.Tables;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MedikTapp.Services.AppConfigService
{
    public partial class AppConfigService : ObservableObject
    {
        private AppConfig _appConfig;
        private readonly DatabaseService.DatabaseService _databaseService;
        private readonly DelegateWeakEventManager _appConfigInitialized;
        public event EventHandler AppConfigInitialized
        {
            add => _appConfigInitialized.AddEventHandler(value);
            remove => _appConfigInitialized.RemoveEventHandler(value);
        }

        public AppConfigService(DatabaseService.DatabaseService databaseService)
        {
            _databaseService = databaseService;
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
                    PatientId = 0,
                    Email = string.Empty,
                    Password = string.Empty,
                    PatientName = string.Empty,
                    IsBiometricLoginEnabled = false,
                    IsDarkModeEnabled = false,
                    IsPromoNotifEnabled = false
                });
            }

            var config = await _databaseService.FindSingle<AppConfig>();
            PatientId = config.PatientId;
            Password = config.Password;
            Email = config.Email;
            IsBiometricLoginEnabled = config.IsBiometricLoginEnabled;
            PatientName = config.PatientName;
            IsDarkModeEnabled = config.IsDarkModeEnabled;
            IsPromoNotifEnabled = config.IsPromoNotifEnabled;

            _appConfigInitialized.RaiseEvent(this, EventArgs.Empty, nameof(AppConfigInitialized));
        }

        public async Task UpdateConfig(string propertyName, object value)
        {
            await _databaseService.Execute($"UPDATE AppConfig SET {propertyName} = ?", value);

            var config = await _databaseService.FindSingle<AppConfig>();
            PatientId = config.PatientId;
            Password = config.Password;
            Email = config.Email;
            IsBiometricLoginEnabled = config.IsBiometricLoginEnabled;
            PatientName = config.PatientName;
            IsDarkModeEnabled = config.IsDarkModeEnabled;
            IsPromoNotifEnabled = config.IsPromoNotifEnabled;
        }
    }
}