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
        Task<PatientModel> Login([Body(BodySerializationMethod.Serialized)] Dictionary<string, string> data);

        [Headers("x-functions-key : chWkam7CzIRFZ2gTGrj6h5v_v_H4meHNu1GBK1RnIthmAzFuIUanpA==")]
        [Post("/api/Register")]
        Task<PatientModel> Register([Body(BodySerializationMethod.Serialized)] Dictionary<string, string> data);
        #endregion

        #region Services
        [Headers("x-functions-key : bfVH99ZkhaBIiY9KKSbrfn-MuQQjJ9gpad2-eDJVdkU3AzFu94stKg==")]
        [Get("/api/GetService")]
        Task<IEnumerable<Models.Services>> GetService();
        #endregion

        #region Appointments
        [Headers("x-functions-key : 6tB18tYTeZSEKMRqmCSTJdBGBlldwkczivQ4rycK4scWAzFu9ysfkQ==")]
        [Post("/api/AddAppointment")]
        Task AddAppointment([Body(BodySerializationMethod.Serialized)] AppointmentModel appointment);

        [Headers("x-functions-key : FgwBKouW3y3ZFCYbU-C7U4JNfsnHttUta43AOWFerWj5AzFuBiw_Fg==")]
        [Get("/api/GetAppointmentAvailableTime?serviceId={serviceId}&year={year}&month={month}&day={day}")]
        Task<IEnumerable<DateTime>> GetAppointmentAvailableTime(int serviceId, int year, int month, int day);

        [Headers("x-functions-key : I012SyoY8TeDb7hH5X-2XQTP1Qo7OplWNpcowK_TFXMhAzFuQg49cg==")]
        [Get("/api/GetAppointmentsByPatientId?patientId={patientId}")]
        Task<IEnumerable<Models.ScheduleModel>> GetAppointmentsByPatientId(int patientId);
        #endregion
    }
}
//https://mediktapp.azurewebsites.net/api/GetAppointmentsByPatientId?code=