using MedikTapp.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedikTapp.Services.HttpService
{
    public interface IMedikTappApi
    {
        [Headers("x-functions-key : PFNUfmsfwfm5S-TlTuzQuKp1USSHLwBIfKHCeJrBSe8HAzFuPHVrJA==")]
        [Post("/api/Login")]
        Task<PatientModel> Login([Body(BodySerializationMethod.Serialized)] Dictionary<string, string> data);

        [Headers("x-functions-key : chWkam7CzIRFZ2gTGrj6h5v_v_H4meHNu1GBK1RnIthmAzFuIUanpA==")]
        [Post("/api/Register")]
        Task<string> Register([Body(BodySerializationMethod.Serialized)] Dictionary<string, string> data);
    }

    public class Test
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}