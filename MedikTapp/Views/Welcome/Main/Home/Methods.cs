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

            ServicesCollection = new(_medikTappService.MedikTappServices.Take(6));
            IsLoadingData = false;
        }

        private void InitPromos()
        {
            //Promos = new(_medikTappService.MedikTappPromos);

            Promos = new()
            {
                new()
                {
                    StartDate = new DateTime(2022, 6, 12, 8, 15, 0),
                    PromoDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    PromoImage = "promo1.jpg",
                    PromoName = "Ultrasound (Promo)",
                    PromoPrice = 850,
                },
                new()
                {
                    StartDate = new DateTime(2022, 6, 13, 8, 30, 0),
                    PromoDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    PromoImage = "promo2.jpg",
                    PromoName = "Executive Health Checkup",
                    PromoPrice = 1500,
                },
                new()
                {
                    StartDate = new DateTime(2022, 6, 12, 8, 15, 0),
                    PromoDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    PromoImage = "promo1.jpg",
                    PromoName = "Ultrasound (Promo)",
                    PromoPrice = 850,
                },
                new()
                {
                    StartDate = new DateTime(2022, 6, 13, 8, 30, 0),
                    PromoDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    PromoImage = "promo2.jpg",
                    PromoName = "Executive Health Checkup",
                    PromoPrice = 1500,
                },
            };
        }
    }
}