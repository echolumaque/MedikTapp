using Newtonsoft.Json;
using System.IO;

namespace MedikTappFunctionApp.Services
{
    public class JsonService
    {
        public T ReadJsonRequestMessage<T>(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            using JsonReader jsonReader = new JsonTextReader(streamReader);
            var serializer = new JsonSerializer();

            return serializer.Deserialize<T>(jsonReader);
        }
    }
}