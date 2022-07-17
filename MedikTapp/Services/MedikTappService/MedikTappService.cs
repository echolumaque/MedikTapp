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
        public IEnumerable<Models.PromoModel> MedikTappPromos { get; set; }
        public IEnumerable<Models.PromoModel> MedikTappPromoNotifications { get; set; }

        public async Task Init()
        {
            MedikTappServices = await _httpService.GetServices();
            //MedikTappPromos = await _httpService.GetPromos();
            //MedikTappPromoNotifications = await _httpService.GetPromoNotifications();
        }
    }
}