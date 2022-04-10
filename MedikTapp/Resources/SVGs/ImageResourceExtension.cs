using FFImageLoading.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedikTapp.Resources.SVGs
{
    [ContentProperty(nameof(FileName))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string FileName { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider) => FileName.EndsWith(".svg")
            ? new EmbeddedResourceImageSource(new($"resource://MedikTapp.Resources.SVGs.{FileName}.svg"))
            : ImageSource.FromResource($"MedikTapp.Resources.SVGs.{FileName}.png", Application.Current.GetType().Assembly);
    }
}