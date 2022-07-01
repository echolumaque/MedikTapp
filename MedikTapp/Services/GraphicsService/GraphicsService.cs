using FFImageLoading;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MedikTapp.Services.GraphicsService
{
    public class GraphicsService
    {
        private readonly IImageService _imageService;

        public GraphicsService(IImageService imageService) => _imageService = imageService;

        public async Task PreloadImages()
        {
            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .Where(x => x.StartsWith("MedikTapp.Resources.Images"));

            foreach (var item in resources)
            {
                await _imageService.LoadEmbeddedResource($"{item}", typeof(App).GetTypeInfo().Assembly)
                    .DownSample()
                    .PreloadAsync()
                    .ConfigureAwait(false);
            }
        }
    }
}