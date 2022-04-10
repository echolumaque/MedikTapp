using MedikTapp.Services.NavigationService;
using MedikTapp.Views.MainPage;
using MedikTapp.Views.MainPage.Bookings;
using MedikTapp.Views.MainPage.Home;
using MedikTapp.Views.MainPage.Schedule;
using MedikTapp.Views.MainPage.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace MedikTapp.DI
{
    public static class DependencyInjectionContainer
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<App>()
                .AddSingleton<NavigationService>()

                .AddTransient<MainPageViewModel>()
                .AddTransient<HomeTabViewModel>()
                .AddTransient<BookingsTabViewModel>()
                .AddTransient<ScheduleTabViewModel>()
                .AddTransient<SettingsTabViewModel>();
        }
    }
}