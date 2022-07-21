using FFImageLoading.Forms;
using System;
using System.IO;
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
            if (bindable is not CachedImage image || newValue is null)
                return;

            var imageValue = newValue.ToString();
            var buffer = new Span<byte>(new byte[newValue.ToString().Length]);
            var isValidBase64String = Convert.TryFromBase64String(imageValue, buffer, out var _);
            if (isValidBase64String)
            {
                var base64ByteArray = Convert.FromBase64String(imageValue);
                image.Source = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(base64ByteArray));
            }
            else
            {
                image.Source = new EmbeddedResourceImageSource(new($"resource://MedikTapp.Resources.Images.{newValue}"));
            }
        }
    }
}