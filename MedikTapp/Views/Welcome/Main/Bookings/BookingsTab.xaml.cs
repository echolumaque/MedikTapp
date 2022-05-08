using MedikTapp.Templates.Base;

namespace MedikTapp.Views.Welcome.Main.Bookings
{
    public partial class BookingsTab : BaseTab<BookingsTabViewModel>
    {
        public BookingsTab(BookingsTabViewModel vm) : base(vm)
        {
            InitializeComponent();
        }
    }
}