using MedikTapp.Constants;
using MedikTapp.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<IEnumerable<PatientModel>> Login(Test test) =>
            _medikTappApi.Login(test);

        public Task<string> Register(string name, string email, string password) =>
            _medikTappApi.Register(name, email, password);
    }
}