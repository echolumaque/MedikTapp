using MedikTapp.Services.DatabaseService;
using MedikTapp.Services.MockService;
using MedikTapp.Services.NavigationService;
using MedikTapp.Services.NotificationService;
using MedikTapp.Views.MainPage;
using MedikTapp.Views.Onboarding;
using MedikTapp.Views.Onboarding.Account;
using MedikTapp.Views.Welcome.Main.Bookings;
using MedikTapp.Views.Welcome.Main.Home;
using MedikTapp.Views.Welcome.Main.Home.Products;
using MedikTapp.Views.Welcome.Main.Home.ServiceConfirmation;
using MedikTapp.Views.Welcome.Main.Schedule;
using MedikTapp.Views.Welcome.Main.Schedule.Calendar;
using MedikTapp.Views.Welcome.Main.Settings;
using Microsoft.Extensions.DependencyInjection;
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

                .AddTransient<MainPageViewModel>()
                .AddTransient<HomeTabViewModel>()
                .AddTransient<BookingsTabViewModel>()
                .AddTransient<ScheduleTabViewModel>()
                .AddTransient<ServiceConfirmationPopupViewModel>()
                .AddTransient<ProductsPageViewModel>()
                .AddTransient<CalendarPopupViewModel>()
                .AddTransient<SettingsTabViewModel>()
                .AddTransient<OnboardingPageViewModel>()
                .AddTransient<AccountPageViewModel>();
        }
    }
}