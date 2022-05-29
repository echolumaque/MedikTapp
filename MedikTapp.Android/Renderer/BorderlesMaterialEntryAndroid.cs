using Android.Content;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlesMaterialEntry), typeof(BorderlesMaterialEntryAndroid), new[] { typeof(VisualMarker.MaterialVisual) })]
namespace MedikTapp.Droid.Renderer
{
    public class BorderlesMaterialEntryAndroid : MaterialEntryRenderer
    {
        public BorderlesMaterialEntryAndroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BoxStrokeWidth = 0;
                Control.BoxStrokeWidthFocused = 0;
            }
        }
    }
}