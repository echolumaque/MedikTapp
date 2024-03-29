﻿using Android.Content;
using MedikTapp.Droid.Renderer;
using MedikTapp.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessMaterialEntry), typeof(BorderlesMaterialEntryAndroid),
    new[] { typeof(VisualMarker.MaterialVisual) })]
namespace MedikTapp.Droid.Renderer
{
    public class BorderlesMaterialEntryAndroid : MaterialEntryRenderer
    {
        public BorderlesMaterialEntryAndroid(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BoxStrokeWidth = 0;
                Control.BoxStrokeWidthFocused = 0;
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