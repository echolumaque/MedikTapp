using MedikTapp.Constants;
using MedikTapp.Models;
using Refit;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MedikTapp.Services.HttpService
{
    public class HttpService
    {
        private readonly IMedikTappApi _medikTappApi;

        public HttpService()
        {
            _medikTappApi = RestService.For<IMedikTappApi>(MedikTappRemoteKeys.FunctionUrl,
                new RefitSettings(new SystemTextJsonContentSerializer()));
        }

        public ConfiguredTaskAwaitable<PatientModel> Login(Dictionary<string, string> data) =>
            _medikTappApi.Login(data).ConfigureAwait(false);

        public ConfiguredTaskAwaitable<string> Register(Dictionary<string, string> data) =>
            _medikTappApi.Register(data).ConfigureAwait(false);
    }
}