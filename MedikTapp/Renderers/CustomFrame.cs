using Xamarin.Forms;

namespace MedikTapp.Renderers
{
    public class CustomFrame : Frame
    {
        public CustomFrame()
        {
            CornerRadius = 0;

        }

        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
            nameof(CornerRadius),
            typeof(CornerRadius),
            typeof(CustomFrame),
            defaultValue: default);
        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}