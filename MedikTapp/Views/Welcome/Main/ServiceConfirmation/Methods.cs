using MedikTapp.Services.NavigationService;
using MedikTapp.ViewModels.Base;
using System.Threading.Tasks;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopupViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            //todo here
            var passedService = parameters.GetValue<Models.Services>("service");
            _serviceId = passedService.ServiceId;
            ServiceImage = passedService.ServiceImage;
            ServiceName = passedService.ServiceName;
            ServiceDescription = passedService.ServiceDescription;
            ServicePrice = passedService.ServicePrice;
            //ProductImageSize = passedService.IsPromo
            //    ? Tuple.Create(200, 250)
            //    : Tuple.Create(100, 100);
        }

        private async Task AddToBooking()
        {
            await _databaseService.Insert(new Models.Services
            {
                ServiceDescription = ServiceDescription,
                ServiceImage = ServiceImage,
                ServiceName = ServiceName,
                ServicePrice = ServicePrice,
                ServiceId = _serviceId
            });

            var mainPageBindingContext = (TabMainPageViewModelBase)NavigationService.GetCurrentPage().BindingContext;
            mainPageBindingContext.Tabs[2].CanHaveBadge = true;
            mainPageBindingContext.Tabs[2].BadgeCount = await _databaseService.FindCount<Models.Services>().ConfigureAwait(false);
            await NavigationService.PopPopup();
        }
    }
}