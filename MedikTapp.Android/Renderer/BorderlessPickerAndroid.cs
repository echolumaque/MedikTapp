using Android.Content;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessPicker), typeof(BorderlessPickerAndroid))]
namespace MedikTapp.Droid.Renderer
{
    public class BorderlessPickerAndroid : PickerRenderer
    {
        public BorderlessPickerAndroid(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
            }
        }
    }
}