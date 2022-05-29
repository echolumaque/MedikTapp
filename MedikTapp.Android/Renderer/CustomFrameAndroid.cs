using Android.Content;
using Android.Graphics.Drawables;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FrameRenderer = Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameAndroid))]
namespace MedikTapp.Droid.Renderer
{

    public class CustomFrameAndroid : FrameRenderer
    {
        public CustomFrameAndroid(Context context) : base(context) { }
        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement is CustomFrame)
                UpdateCornerRadius();
        }
        private void UpdateCornerRadius()
        {
            if (Control.Background is GradientDrawable gradientDrawable)
            {
                var cornerRadius = (Element as CustomFrame)?.CornerRadius;

                if (!cornerRadius.HasValue)
                {
                    return;
                }


                var topLeftCorner = Context.ToPixels(cornerRadius.Value.TopLeft);
                var topRightCorner = Context.ToPixels(cornerRadius.Value.TopRight);
                var bottomLeftCorner = Context.ToPixels(cornerRadius.Value.BottomLeft);
                var bottomRightCorner = Context.ToPixels(cornerRadius.Value.BottomRight);


                gradientDrawable.SetCornerRadii(new float[8]
             {
                topLeftCorner,
                topLeftCorner,


                 topRightCorner,
                 topRightCorner,


                  bottomRightCorner,
                  bottomRightCorner,

                  bottomLeftCorner,
                  bottomLeftCorner
             });
            }

        }
    }
}