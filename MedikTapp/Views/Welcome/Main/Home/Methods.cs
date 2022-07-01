using MedikTapp.Services.NavigationService;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel
    {
        public override async void Initialized(NavigationParameters parameters)
        {
            InitPromos();
            ServicesCollection = new((await _httpService.GetServices()).Take(6));

            if (!_isAlreadyTimed)
            {
                await Task.Delay(2000);
                Device.StartTimer(TimeSpan.FromSeconds(3), () =>
                {
                    PromoPosition = PromoPosition < 3
                        ? PromoPosition += 1
                        : PromoPosition = 0;
                    return true;
                });
                _isAlreadyTimed = true;
            }

            IsLoadingData = false;
        }

        private void InitPromos()
        {
            Promos = new()
            {
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 12, 8, 15, 0),
                    ServiceDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    ServiceImage = "promo1.jpg",
                    ServiceName = "Ultrasound (Promo)",
                    ServicePrice = 850,
                    IsPromo = true
                },
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 13, 8, 30, 0),
                    ServiceDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    ServiceImage = "promo2.jpg",
                    ServiceName = "Executive Health Checkup",
                    ServicePrice = 1500,
                    IsPromo = true
                },
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 12, 8, 45, 0),
                    ServiceDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    ServiceImage = "promo1.jpg",
                    ServiceName = "Ultrasound (Promo)",
                    ServicePrice = 850,
                    IsPromo = true
                },
                new()
                {
                    AvailableTime = new DateTime(2022, 6, 13, 9, 0, 0),
                    ServiceDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    ServiceImage = "promo2.jpg",
                    ServiceName = "Executive Health Checkup",
                    ServicePrice = 1500,
                    IsPromo = true
                },
            };
        }
    }
}