using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Helpers;

namespace MedikTapp.Services.MedikTappService
{
    public class MedikTappService
    {
        private readonly HttpService.HttpService _httpService;
        private readonly DelegateWeakEventManager _medikTappServiceInitialized;

        public MedikTappService(HttpService.HttpService httpService)
        {
            _httpService = httpService;
            _medikTappServiceInitialized = new DelegateWeakEventManager();
        }

        public event EventHandler MedikTappServiceInitialized
        {
            add => _medikTappServiceInitialized.AddEventHandler(value);
            remove => _medikTappServiceInitialized.RemoveEventHandler(value);
        }

        public IEnumerable<Models.Services> MedikTappServices { get; set; }
        public IEnumerable<Models.Services> MedikTappPromos { get; set; }

        public async Task Init()
        {
            MedikTappServices = await _httpService.GetServices();
            MedikTappPromos = await _httpService.GetPromos();
            _medikTappServiceInitialized.RaiseEvent(this, EventArgs.Empty, nameof(MedikTappServiceInitialized));
        }
    }
}