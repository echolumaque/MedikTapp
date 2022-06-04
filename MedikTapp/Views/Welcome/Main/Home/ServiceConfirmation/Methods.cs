using MedikTapp.Services.NavigationService;
using System;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.Home.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            var passedService = parameters.GetValue<Models.Services>("service");
            EarliestAvailableDate = passedService.EarliestAvailableDate;
            ServiceImagePath = passedService.ServiceImagePath;
            ServiceName = passedService.ServiceName;
            ServiceDescription = passedService.ServiceDescription;
            ServicePrice = passedService.ServicePrice;
            ProductImageSize = passedService.IsPromo
                ? Tuple.Create(200, 250)
                : Tuple.Create(80, 80);
        }

        private async Task AddToBooking()
        {
            await _databaseService.Insert(new Tables.Bookings
            {
                EarliestAvailableDate = EarliestAvailableDate,
                ProductDescription = ServiceDescription,
                ProductImagePath = ServiceImagePath,
                ProductName = ServiceName,
                ProductPrice = ServicePrice,
            });
            await NavigationService.PopPopup();
        }
    }
}