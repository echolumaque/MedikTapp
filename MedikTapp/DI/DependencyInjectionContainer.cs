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
                .AddTransient<SettingsTabViewModel>();
                .AddTransient<OnboardingPageViewModel>()
                .AddTransient<AccountPageViewModel>();
        }
    }
}