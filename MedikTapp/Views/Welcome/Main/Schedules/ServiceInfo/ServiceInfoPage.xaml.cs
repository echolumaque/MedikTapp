using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPage : BasePage<ServiceInfoPageViewModel>
    {
        public ServiceInfoPage(ServiceInfoPageViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}