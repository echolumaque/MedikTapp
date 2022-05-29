using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Onboarding;
using MedikTapp.Views.Onboarding.Account;
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

                .AddTransient<OnboardingPageViewModel>()
                .AddTransient<AccountPageViewModel>();
        }
    }
}