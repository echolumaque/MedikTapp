using Android.Content;
using Android.Graphics.Drawables;
using MedikTapp.Droid.Renderers;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Color = Android.Graphics.Color;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry),
   typeof(BorderlessEntryAndroid))]
namespace MedikTapp.Droid.Renderers
{
    public class BorderlessEntryAndroid : EntryRenderer
    {
        public BorderlessEntryAndroid(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(Color.Transparent);
                Control.SetBackground(gradientDrawable);

            }
        }
    }
}