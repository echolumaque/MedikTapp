using Microsoft.Extensions.DependencyInjection;
using System;

namespace MedikTapp.DI
{
    public static class Startup
    {
        public static IServiceProvider Init(Action<IServiceCollection> platformSpecificServices = null)
        {
            ServiceCollection services = new();
            platformSpecificServices?.Invoke(services);
            services.ConfigureServices();

            return services.BuildServiceProvider();
        }
    }
}