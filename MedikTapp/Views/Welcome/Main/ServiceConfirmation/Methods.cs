using MedikTapp.Services.NavigationService;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            var passedService = parameters.GetValue<Models.Services>("service");
            _base64String = passedService.ServiceImage;
            EarliestAvailableDate = passedService.AvailableTime;
            ServiceImage = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(_base64String)));
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
                ServiceImage = _base64String,
                ServiceName = ServiceName,
                ServicePrice = ServicePrice
            });
            await NavigationService.PopPopup();
        }
    }
}