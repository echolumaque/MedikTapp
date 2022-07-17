using MedikTapp.Constants;
using MedikTapp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public Task<PatientModel> Login(PatientModel patient)
        {
            return _medikTappApi.Login(patient);
        }

        public Task Register(PatientModel patient)
        {
            return _medikTappApi.Register(patient);
        }

        public Task<IEnumerable<Models.Services>> GetServices()
        {
            return _medikTappApi.GetService();
        }

        public ConfiguredTaskAwaitable<int> AddAppointment(AddAppointmentModel appointment)
        {
            return _medikTappApi.AddAppointment(appointment).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<DateTime>> GetAppointmentAvailableTime(int serviceId, int year, int month, int day)
        {
            return _medikTappApi.GetAppointmentAvailableTime(serviceId, year, month, day).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<AppointmentModel>> GetAppointmentsByPatientId(int patientId)
        {
            return _medikTappApi.GetAppointmentsByPatientId(patientId).ConfigureAwait(false);
        }

        //todo here
        //public ConfiguredTaskAwaitable<IEnumerable<Models.PromoModel>> GetPromos()
        //{
        //    return _medikTappApi.GetPromos().ConfigureAwait(false);
        //}
    }
}