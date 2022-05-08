using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedikTapp.Templates
{
    [ContentProperty(nameof(FileName))]
    public class ImageExtension : ContentView, IMarkupExtension<ImageSource>
    {
        public static readonly BindableProperty FileNameProperty = BindableProperty.Create(
            nameof(FileName),
            typeof(string),
            typeof(ImageExtension),
            defaultValue: default);
        public string FileName
        {
            get => (string)GetValue(FileNameProperty);
            set => SetValue(FileNameProperty, value);
        }

        public ImageSource ProvideValue(IServiceProvider serviceProvider) =>
            ImageSource.FromResource($"MedikTapp.Resources.SVGs.{FileName}.png", typeof(App).GetTypeInfo().Assembly);

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) =>
            (this as IMarkupExtension<ImageSource>).ProvideValue(serviceProvider);
    }
}