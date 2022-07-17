using Newtonsoft.Json;
using System.IO;

namespace MedikTappFunctionApp.Services
{
    public class JsonService
    {
        private readonly JsonSerializer _serializer;

        public JsonService()
        {
            _serializer = new JsonSerializer();
        }

        public T ReadJsonRequestMessage<T>(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            using JsonReader jsonReader = new JsonTextReader(streamReader);

            return _serializer.Deserialize<T>(jsonReader);
        }
    }
}