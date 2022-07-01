using MedikTapp.Services.NavigationService;
using System;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            var passedService = parameters.GetValue<Models.Services>("service");
            EarliestAvailableDate = passedService.AvailableTime;
            ServiceImage = passedService.ServiceImage;
            ServiceName = passedService.ServiceName;
            ServiceDescription = passedService.ServiceDescription;
            ServicePrice = passedService.ServicePrice;
            ProductImageSize = passedService.IsPromo
                ? Tuple.Create(200, 250)
                : Tuple.Create(100, 100);
        }

        private async Task AddToBooking()
        {
            await _databaseService.Insert(new Models.Services
            {
                AvailableTime = EarliestAvailableDate,
                ServiceDescription = ServiceDescription,
                ServiceImage = ServiceImage,
                ServiceName = ServiceName,
                ServicePrice = ServicePrice
            });
            await NavigationService.PopPopup();
        }
    }
}