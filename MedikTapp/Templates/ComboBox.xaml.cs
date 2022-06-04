using Xamarin.Forms;

namespace MedikTapp.Templates
{
    public partial class ComboBox : ContentView
    {
        public ComboBox()
        {
            InitializeComponent();
            Content.BindingContext = this;
        }
    }
}