using MedikTappFunctionApp;
using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace MedikTappFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddDbContext<EntityContext>(options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, Environment.GetEnvironmentVariable("SQLServerConnectionString")))
                .AddSingleton<JsonService>()
                .AddScoped<SqlService>();
        }
    }
}