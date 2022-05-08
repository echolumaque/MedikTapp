using MedikTapp.Models;
using MedikTapp.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;

namespace MedikTapp.Services.MockService
{
    public partial class MockService
    {
        private readonly DelegateWeakEventManager _onMockServiceInitialized;

        public MockService()
        {
            _onMockServiceInitialized = new();
        }

        public Task Init()
        {
            var tasks = Task.WhenAll
            (
                ReadMockBookings(),
                ReadMockSchedules(),
                ReadMockServices()
            );
            _onMockServiceInitialized.RaiseEvent(this, EventArgs.Empty, nameof(OnMockServiceInitialized));

            return tasks;
        }

        private async Task ReadMockBookings()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockBookings.json"));
            MockBookings = JsonConvert.DeserializeObject<IEnumerable<Bookings>>(await reader.ReadToEndAsync());
        }

        private async Task ReadMockSchedules()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockSchedules.json"));
            MockSchedules = JsonConvert.DeserializeObject<IEnumerable<Schedules>>(await reader.ReadToEndAsync());
        }

        private async Task ReadMockServices()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockServices.json"));
            MockServices = JsonConvert.DeserializeObject<IEnumerable<Models.Services>>(await reader.ReadToEndAsync());
        }
    }
}