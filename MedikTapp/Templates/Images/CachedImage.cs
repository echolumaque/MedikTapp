using FFImageLoading.Forms;
using System;
using Xamarin.Forms;

namespace MedikTapp.Templates.Images
{
    public class CachedImage : FFImageLoading.Forms.CachedImage
    {
        public CachedImage()
        {
            BitmapOptimizations = true;
            CacheDuration = TimeSpan.FromDays(365);
        }

        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(CachedImage),
            defaultValue: default,
            propertyChanged: OnSourcePropertyChanged);
        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        private static void OnSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not CachedImage image)
                return;

            image.Source = new EmbeddedResourceImageSource(new($"resource://MedikTapp.Resources.Images.{newValue}"));
        }
    }
}