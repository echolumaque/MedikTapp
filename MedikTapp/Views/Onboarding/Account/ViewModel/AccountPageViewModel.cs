using MedikTapp.Helpers.Command;
using MedikTapp.Interfaces;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MedikTapp.Views.Onboarding.Account
{
    public partial class AccountPageViewModel : ViewModelBase
    {
        private const string TermsAndConditions = "These terms and conditions constitute a legally binding agreement made between you and MedikTapp concerning your access to and use of our mobile application, products available to you through the application, as well as services we may provide through this application. You agree that by using the service, you have read, understood, and agree to be bound by all of these Terms and Conditions. If you do not agree with these, then you may not access or use the application.\n\nNo information provided by MedikTapp constitutes the practice of medicine or any other medical profession. MedikTapp does not recommend or endorse any specific tests, products, or procedures. MedikTapp is not for medical emergencies or urgent situations. If you have an emergency, please go to your nearest hospital and seek immediate assistance from medical emergency personnel. The services provided are not intended to be a substitute or replacement for professional medical advice, diagnosis or treatment.\n\nThis Agreement applies to your use and your registration for, subscription to, and use of MedikTapp. You acknowledge that you understand, have read and accepted all terms and conditions in this Agreement and those that are contained in the Data Privacy Policy, which shall form an integral part of this Agreement.\n\n1. Age\nYou represent and warrant that you are at least eighteen (18) years of age, and have the capacity to bind yourself contractually to this Agreement and to use MedikTapp. If you are under eighteen (18) years of age, or otherwise incapacitated to enter into this Agreement, you may not register or use MedikTapp.\n\n2. Availment of Service / Use of the Application\nYou will avail of the Service and use the Application only for legitimate and lawful purposes, and in accordance with these Terms and Conditions, you will ensure that any information you provide to us in availing the service or through using the application is true, accurate, current, and complete.\n\nThe availment of our service or the use of application is not intended or appropriate for emergency or serious cases. If you think you have a medical or mental health emergency, or if, at any time you are concerned, call a doctor or emergency phone number immediately or go to the nearest open clinic or emergency room.\n\n3. Location\nThe access to and use of MedikTapp is limited to natural persons located in the Philippines. For patients who will use the application outside the Philippines, you warrant that the availment of medical services from physicians in the Philippines and the use of MedikTapp services are in compliance with the laws of the country where you are situated. That in case of conflict, you warrant that PUDC, its successors-in-interest, stockholders, officers, directors, agents, or employees, shall not be made a party to such litigation and shall not be liable in any capacity.\n\n4. Registration of Personal Information and Medical History\nYou must register to use MedikTapp. During registration and use of MedikTapp, you will be required to provide certain personal information, including your name, address, phone number, and other personal circumstance (“Personal Details”). It is your sole responsibility to update MedikTapp of changes to your Personal Details, and your other personal information so that all your records are current, complete and accurate and such changes shall form part of your records, for access and use by your physician/s.";
        private const string PrivacyPolicy = "MedikTapp’s privacy policy (the Privacy Policy) as may amended from time to time and may be accessible through in the application is hereby incorporated by reference to these Terms and Conditions. The Privacy Policy explains how we use the information you disclose to MedikTapp in availing of the service and in using the application. MedikTapp takes utmost care to never disclose your data and information, except upon your request, or if the same is necessary to comply with legal, statutory, or investigative law enforcement requirements, or as otherwise provided in the privacy policy changes.\n\nWe reserve the right, in our sole discretion, to amend these terms and conditions, or to make changes to the functionality, features, or content of the application or service at any time and for any reason. You will be notified of any changes via email, or via the posting of an updated version. If you do not agree with any aspect of the updated terms and conditions, application and/or service, you must immediately cease availing the service or using the application.";
        private readonly AppConfigService _appConfigService;
        private readonly IConnectivity _connectivity;
        private readonly IFingerprint _fingerprint;
        private readonly HttpService _httpService;
        private readonly IMainThread _mainThread;
        private readonly IPreferences _preferences;
        private readonly IToast _toast;

        public AccountPageViewModel(NavigationService navigationService,
            IMainThread mainThread,
            IConnectivity connectivity,
            IFingerprint fingerprint,
            HttpService httpService,
            AppConfigService appConfigService,
            IPreferences preferences,
            IToast toast) : base(navigationService)
        {

            _appConfigService = appConfigService;
            _connectivity = connectivity;
            _fingerprint = fingerprint;
            _httpService = httpService;
            _toast = toast;
            _preferences = preferences;
            _mainThread = mainThread;

            _appConfigService.AppConfigInitialized += AppConfigInitialized;

            BiometricsCmd = new AsyncSingleCommand(BiometricsLogin, () => IsBiometricsAvailable);
            LoginCmd = new AsyncSingleCommand(() => Login(false), () => AllLoginValidations());
            ChangeTemplateCmd = new Command<string>(key => AccountPageTemplate = key);
            RegisterCmd = new AsyncSingleCommand(Register, () => AllRegistrationValidatons());
            JoinUsCmd = new AsyncSingleCommand<ScrollView>(JoinUs);
            PrivacyPolicyCmd = new AsyncSingleCommand(ShowPrivacyPolicy);
            TermsAndConditionsCmd = new AsyncSingleCommand(ShowTermsAndConditions);
        }
    }
}