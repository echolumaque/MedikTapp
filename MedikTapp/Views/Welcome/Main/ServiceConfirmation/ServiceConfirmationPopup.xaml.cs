using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.ServiceConfirmation
{
    public partial class ServiceConfirmationPopup : BasePopup<ServiceConfirmationPopupViewModel>
    {
        public ServiceConfirmationPopup(ServiceConfirmationPopupViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}