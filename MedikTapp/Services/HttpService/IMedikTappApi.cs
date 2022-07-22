using MedikTapp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedikTapp.Services.HttpService
{
    public interface IMedikTappApi
    {
        #region Login, Change Password, and Register
        [Headers("x-functions-key : PFNUfmsfwfm5S-TlTuzQuKp1USSHLwBIfKHCeJrBSe8HAzFuPHVrJA==")]
        [Post("/api/Login")]
        Task<PatientModel> Login([Body(BodySerializationMethod.Serialized)] PatientModel data);

        [Headers("x-functions-key : chWkam7CzIRFZ2gTGrj6h5v_v_H4meHNu1GBK1RnIthmAzFuIUanpA==")]
        [Post("/api/Register")]
        Task Register([Body(BodySerializationMethod.Serialized)] PatientModel data);

        [Headers("x-functions-key : Oj3TkL-FCISUhdHOHdv650RoPjmXb-En0VDzGc7GQo8CAzFunPiHRw==")]
        [Put("/api/ChangePassword?patientId={patientId}&newPassword={newPassword}")]
        Task ChangePassword(int patientId, string newPassword);
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

        [Headers("x-functions-key : Cu3qBfOxsL_8tMEy_tKVdSOwk14SNAACx8wzZQ1ey2hTAzFuIYzn4Q==")]
        [Put("/api/CancelAppointment?appointmentId={appointmentId}")]
        Task CancelAppointment(int appointmentId);

        [Headers("x-functions-key : jfCSyl4pRv8dU7lxYjLjroFYDoYX1IuEhzrMF4vVhK6hAzFuw0d-TQ==")]
        [Get("/api/GetServiceAppointmentAvailableTimes?serviceId={serviceId}&year={year}&month={month}&day={day}")]
        Task<IEnumerable<DateTime>> GetAppointmentAvailableTime(int serviceId, int year, int month, int day);

        [Headers("x-functions-key : zMZVXJOEC-aCvRBztkCjYk-74bOiVQg8MPDS3MR1UlHhAzFu-pk0rA==")]
        [Get("/api/GetPatientUpcomingAppointment?patientId={patientId}")]
        Task<IEnumerable<AppointmentModel>> GetPatientUpcomingAppointment(int patientId);

        [Headers("x-functions-key : xp4_PxUeQUFB08FjfUiTMWSAD_wq94_urpw-1jzzXyh4AzFu7_eoyg==")]
        [Get("/api/GetPatientCancelledAppointment?patientId={patientId}")]
        Task<IEnumerable<AppointmentModel>> GetPatientCancelledAppointment(int patientId);

        [Headers("x-functions-key : tfnC85f1Tit1O2d9Jco-3j7LEn4PUyEUwUZk2Wws52FuAzFuVBmZ8A==")]
        [Get("/api/GetPatientCompletedAppointment?patientId={patientId}")]
        Task<IEnumerable<AppointmentModel>> GetPatientCompletedAppointment(int patientId);

        [Headers("x-functions-key : edHb3aS9Vz8H64OsKZfMlK-adW6cThs5SubCAPMtkKRGAzFuVcBKVA==")]
        [Put("/api/RescheduleAppointment")]
        Task<int> RescheduleAppointment(AddAppointmentModel appointment);
        #endregion

        #region Promos
        [Headers("x-functions-key : n8UzakNdDPgEFSjkFZv9ZQl9qsc0jTanPakzEVz4hCb9AzFuRZ4y8A==")]
        [Get("/api/GetPromo")]
        Task<IEnumerable<Models.Services>> GetPromos();
        #endregion
    }
}