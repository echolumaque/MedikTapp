using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels;
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
                .AddTransient<CartTabViewModel>()
                .AddTransient<onboardingViewModel>()
                .AddTransient<registerViewModel>()
                .AddTransient<loginViewModel>()
                .AddTransient<ScheduleTabViewModel>()
                .AddTransient<SettingsTabViewModel>();
        }
    }
}