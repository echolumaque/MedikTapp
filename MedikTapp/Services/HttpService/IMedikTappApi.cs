using MedikTapp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedikTapp.Services.HttpService
{
    public interface IMedikTappApi
    {
        #region Login and Register
        [Headers("x-functions-key : PFNUfmsfwfm5S-TlTuzQuKp1USSHLwBIfKHCeJrBSe8HAzFuPHVrJA==")]
        [Post("/api/Login")]
        Task<PatientModel> Login([Body(BodySerializationMethod.Serialized)] PatientModel data);

        [Headers("x-functions-key : chWkam7CzIRFZ2gTGrj6h5v_v_H4meHNu1GBK1RnIthmAzFuIUanpA==")]
        [Post("/api/Register")]
        Task Register([Body(BodySerializationMethod.Serialized)] PatientModel data);
        #endregion

        #region Services
        [Headers("x-functions-key : bfVH99ZkhaBIiY9KKSbrfn-MuQQjJ9gpad2-eDJVdkU3AzFu94stKg==")]
        [Get("/api/GetService")]
        Task<IEnumerable<Models.Services>> GetService();
        #endregion

        #region Appointments
        [Headers("x-functions-key : 6tB18tYTeZSEKMRqmCSTJdBGBlldwkczivQ4rycK4scWAzFu9ysfkQ==")]
        [Post("/api/AddAppointment")]
        Task<int> AddAppointment([Body(BodySerializationMethod.Serialized)] AddAppointmentModel appointment);

        [Headers("x-functions-key : jfCSyl4pRv8dU7lxYjLjroFYDoYX1IuEhzrMF4vVhK6hAzFuw0d-TQ==")]
        [Get("/api/GetServiceAppointmentAvailableTimes?serviceId={serviceId}&year={year}&month={month}&day={day}")]
        Task<IEnumerable<DateTime>> GetAppointmentAvailableTime(int serviceId, int year, int month, int day);

        [Headers("x-functions-key : I012SyoY8TeDb7hH5X-2XQTP1Qo7OplWNpcowK_TFXMhAzFuQg49cg==")]
        [Get("/api/GetAppointmentsByPatientId?patientId={patientId}")]
        Task<IEnumerable<AppointmentModel>> GetAppointmentsByPatientId(int patientId);
        #endregion

        #region Promos
        //todo here
        //[Headers("x-functions-key : n8UzakNdDPgEFSjkFZv9ZQl9qsc0jTanPakzEVz4hCb9AzFuRZ4y8A==")]
        //[Get("/api/GetPromo")]
        //Task<IEnumerable<PromoModel>> GetPromos();
        #endregion
    }
}