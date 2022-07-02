using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedikTapp.Services.MedikTappService
{
    public class MedikTappService
    {
        private readonly HttpService.HttpService _httpService;

        public MedikTappService(HttpService.HttpService httpService)
        {
            _httpService = httpService;
        }

        public IEnumerable<Models.Services> MedikTappServices { get; set; }

        public async Task Init()
        {
            MedikTappServices = await _httpService.GetServices();
        }
    }
}