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
            return ReadMockServices();
        }

        private async Task ReadMockServices()
        {
            using StreamReader reader = new(Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MedikTapp.Resources.Data.MockServices.json"));
            MockServices = JsonConvert.DeserializeObject<IEnumerable<Models.Services>>(await reader.ReadToEndAsync().ConfigureAwait(false));
        }
    }
}