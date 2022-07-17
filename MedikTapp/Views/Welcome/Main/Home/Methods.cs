using MedikTapp.Services.NavigationService;
using MedikTapp.Views.Welcome.Main.ServiceConfirmation;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.Home
{
    public partial class HomeTabViewModel
    {
        private string GetImageBase64String(string imageName)
        {
            var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MedikTapp.Resources.Images.{imageName}.jpg");
            var ms = new MemoryStream();
            resource.CopyTo(ms);
            var hehe = Convert.ToBase64String(ms.ToArray());
            return hehe;
        }

        private Task GotoServiceConfirmationPopup(Models.Services service)
        {
            return NavigationService.GoTo<ServiceConfirmationPopup>(new()
            {
                { "service", service }
            });
        }

        private void InitPromos()
        {
            PromosCollection = new()
            {
                new()
                {
                    StartDate = new DateTime(2022, 6, 12, 8, 15, 0),
                    ServiceDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    ServiceImage = GetImageBase64String("promo1"),
                    ServiceName = "Ultrasound (Promo)",
                    ServicePrice = 850,
                },
                new()
                {
                    StartDate = new DateTime(2022, 6, 13, 8, 30, 0),
                    ServiceDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    ServiceImage = GetImageBase64String("promo2"),
                    ServiceName = "Executive Health Checkup",
                    ServicePrice = 1500,
                },
                new()
                {
                    StartDate = new DateTime(2022, 6, 12, 8, 15, 0),
                    ServiceDescription = "Enjoy PUDC's ultrasound promo with 10% off of its original price!",
                    ServiceImage = GetImageBase64String("promo1"),
                    ServiceName = "Ultrasound (Promo)",
                    ServicePrice = 850,
                },
                new()
                {
                    StartDate = new DateTime(2022, 6, 13, 8, 30, 0),
                    ServiceDescription = "Enjoy PUDC's Executive Health Checkup promo for only ₱1,500 from ₱2,992!",
                    ServiceImage = GetImageBase64String("promo2"),
                    ServiceName = "Executive Health Checkup",
                    ServicePrice = 1500,
                },
            };
        }
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
    }
}