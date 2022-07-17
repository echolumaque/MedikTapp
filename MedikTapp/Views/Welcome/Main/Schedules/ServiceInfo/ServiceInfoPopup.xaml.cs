using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Schedule.ServiceInfo
{
    public partial class ServiceInfoPopup : BasePopup<ServiceInfoPopupViewModel>
    {
        public ServiceInfoPopup(ServiceInfoPopupViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}