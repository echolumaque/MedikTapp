using FFImageLoading;
using MedikTapp.Services.AppConfigService;
using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.GraphicsService;
using MedikTapp.Services.HttpService;
using MedikTapp.Services.MedikTappService;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.Views.MainPage;
using MedikTapp.Views.Onboarding;
using MedikTapp.Views.Onboarding.Account;
using MedikTapp.Views.Welcome.Main.Bookings;
using MedikTapp.Views.Welcome.Main.Home;
using MedikTapp.Views.Welcome.Main.Schedule;
using MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
using MedikTapp.Views.Welcome.Main.Services;
using MedikTapp.Views.Welcome.Main.Settings;
using MedikTapp.Views.Welcome.Main.TimeAvailability;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using XF.Services.InitializeDataService;

namespace MedikTapp.DI
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<App>()
                .AddSingleton<NavigationService>()
                .AddSingleton<MockService>()
                .AddSingleton<DatabaseService>()
                .AddSingleton<InitializeDataService>()
                .AddSingleton<NotificationService>()
                .AddSingleton<MedikTappService>()
                .AddSingleton<AppConfigService>()
                .AddSingleton<HttpService>()
                .AddSingleton<GraphicsService>()
                .AddSingleton<IImageService, ImageService>()

                .AddSingleton<IMainThread, MainThreadImplementation>()
                .AddSingleton<IPreferences, PreferencesImplementation>()
                .AddTransient<ServiceInfoPopupViewModel>()
                .AddTransient<MainPageViewModel>()
                .AddTransient<TimeAvailabilityPopupViewModel>()
                .AddTransient<HomeTabViewModel>()
                .AddTransient<ServicesTabViewModel>()
                .AddTransient<BookingsTabViewModel>()
                .AddTransient<ScheduleTabViewModel>()
                .AddTransient<ServiceConfirmationPopupViewModel>()
                .AddTransient<SettingsTabViewModel>()
                .AddTransient<OnboardingPageViewModel>()
                .AddTransient<AccountPageViewModel>();
        }
    }
}