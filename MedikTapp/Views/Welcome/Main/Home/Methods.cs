using MedikTapp.Services.NavigationService;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel
    {
        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!_isAlreadyTimed)
            {
                await AutoScrollPromos();
                _isAlreadyTimed = true;
            }
        }

        private void InitPromos()
        {
            Promos = new()
            {
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 12, 8, 15, 0),
                    ServiceDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    ServiceImagePath = "promo1.jpg",
                    ServiceName = "Ultrasound (Promo)",
                    ServicePrice = 850,
                    IsPromo = true
                },
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 13, 8, 30, 0),
                    ServiceDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    ServiceImagePath = "promo2.jpg",
                    ServiceName = "Executive Health Checkup",
                    ServicePrice = 1500,
                    IsPromo = true
                },
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 12, 8, 45, 0),
                    ServiceDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    ServiceImagePath = "promo1.jpg",
                    ServiceName = "Ultrasound (Promo)",
                    ServicePrice = 850,
                    IsPromo = true
                },
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 13, 9, 0, 0),
                    ServiceDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    ServiceImagePath = "promo2.jpg",
                    ServiceName = "Executive Health Checkup",
                    ServicePrice = 1500,
                    IsPromo = true
                },
            };
        }

        private async Task AutoScrollPromos()
        {
            await Task.Delay(2000);
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                PromoPosition = PromoPosition < 3
                    ? PromoPosition += 1
                    : PromoPosition = 0;
                return true;
            });
        }
    }
}
