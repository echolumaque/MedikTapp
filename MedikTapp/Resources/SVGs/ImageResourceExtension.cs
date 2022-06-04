namespace MedikTapp.Resources.SVGs
{
    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            return ImageSource.FromResource($"MedikTapp.Resources.SVGs.{Source}.png",
                typeof(ImageResourceExtension).GetTypeInfo().Assembly);
        }
    }
}
