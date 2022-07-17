using Android.Content;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;

[assembly: ExportRenderer(typeof(BorderlessDatePicker), typeof(BorderlessDatePickerRenderer))]
namespace MedikTapp.Droid.Renderer
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
        public BorderlessDatePickerRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
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