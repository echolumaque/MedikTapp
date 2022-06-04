using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Services
{
    public partial class ServicesTab : BaseTab<ServicesTabViewModel>
    {
        public ServicesTab(ServicesTabViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}