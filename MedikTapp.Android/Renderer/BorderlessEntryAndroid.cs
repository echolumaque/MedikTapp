using Android.Content;
using Android.Graphics.Drawables;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryAndroid))]
namespace MedikTapp.Droid.Renderer
{
    public class BorderlessEntryAndroid : EntryRenderer
    {
        public BorderlessEntryAndroid(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.Background = new ColorDrawable(Color.Transparent);
        }
    }
}