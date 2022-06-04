using MedikTapp.Models;
using MedikTapp.Tables;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace MedikTapp.Services.MockService
{
    public partial class MockService
    {
        public Task Init()
        {
            return Task.WhenAll
            (
                ReadMockBookings(),
                ReadMockSchedules(),
                ReadMockServices()
            );
        }

        private async Task ReadMockBookings()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockBookings.json"));
            MockBookings = JsonConvert.DeserializeObject<IEnumerable<Bookings>>(await reader.ReadToEndAsync().ConfigureAwait(false));
        }

        private async Task ReadMockSchedules()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockSchedules.json"));
            MockSchedules = JsonConvert.DeserializeObject<IEnumerable<Schedules>>(await reader.ReadToEndAsync().ConfigureAwait(false));
        }

        private async Task ReadMockServices()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockServices.json"));
            MockServices = JsonConvert.DeserializeObject<IEnumerable<Models.Services>>(await reader.ReadToEndAsync().ConfigureAwait(false));
        }
    }
}