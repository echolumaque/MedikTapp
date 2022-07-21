using Android.Content;
using Android.OS;
using Android.Text;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(JustifiedLabel), typeof(JustifiedLabelAndroid))]
namespace MedikTapp.Droid.Renderer
{
    public class JustifiedLabelAndroid : LabelRenderer
    {
        public JustifiedLabelAndroid(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    Control.JustificationMode = JustificationMode.InterWord;
                }
            }
        }
    }
}