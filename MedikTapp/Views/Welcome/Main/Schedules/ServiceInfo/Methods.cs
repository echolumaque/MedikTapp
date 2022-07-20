using MedikTapp.Services.NavigationService;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPopupViewModel
    {
        public override void Initialized(NavigationParameters parameters)
        {
            //todo here
            var passedService = parameters.GetValue<Models.Services>("appointment");
            ServiceImage = passedService.ServiceImage;
            ServiceName = passedService.ServiceName;
            ServiceDescription = passedService.ServiceDescription;
            //ServicePrice = passedService.ServicePrice;
            //ProductImageSize = passedService.IsPromo
            //    ? Tuple.Create(200, 250)
            //    : Tuple.Create(100, 100);
        }

        private Task OpenMap()
        {
            return Launcher.OpenAsync("geo:0,0?q=Parañaque+Ultrasound+Diagnostic+Center");
        }
    }
}