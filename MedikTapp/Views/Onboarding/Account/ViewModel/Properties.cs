using PropertyChanged;
using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel
    {
        #region General Properties
        public string AccountPageTemplate { get; set; }
        public IAsyncCommand BiometricsCmd { get; }
        public ICommand ChangeTemplateCmd { get; }
        public bool IsBiometricsAvailable { get; set; }
        public IAsyncCommand<ScrollView> JoinUsCmd { get; set; }
        public IAsyncCommand LoginCmd { get; }
        public IAsyncCommand RegisterCmd { get; }
        #endregion

        #region Login Properties
        [OnChangedMethod(nameof(OnLoginCredentialsChanged))]
        public string LoginUsername { get; set; }
        [OnChangedMethod(nameof(OnLoginCredentialsChanged))]
        public string LoginPassword { get; set; }
        #endregion

        #region Register Properties
        public bool IsRegisterBirthDateValid { get; set; }
        public bool IsRegisterContactNumberValid { get; set; }
        public bool IsRegisterUsernameValid { get; set; }
        public bool IsRegisterFirstNameValid { get; set; }
        public bool IsRegisterLastNameValid { get; set; }
        public bool IsRegisterPasswordValid { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterAddress { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public DateTime RegisterBirthDate { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterContactNumber { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterUsername { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterFirstName { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterLastName { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterPassword { get; set; }
        [OnChangedMethod(nameof(OnRegisterCredentialsChanged))]
        public string RegisterSex { get; set; }
        #endregion

    }
}