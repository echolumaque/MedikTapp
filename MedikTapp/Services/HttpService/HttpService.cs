using MedikTapp.Constants;
using MedikTapp.Models;
using Refit;
using System;
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

        public ConfiguredTaskAwaitable<PatientModel> Register(Dictionary<string, string> data) =>
            _medikTappApi.Register(data).ConfigureAwait(false);

        public ConfiguredTaskAwaitable<IEnumerable<Models.Services>> GetServices() =>
            _medikTappApi.GetService().ConfigureAwait(false);

        public ConfiguredTaskAwaitable AddAppointment(AppointmentModel appointment)
            => _medikTappApi.AddAppointment(appointment).ConfigureAwait(false);

        public ConfiguredTaskAwaitable<IEnumerable<DateTime>> GetAppointmentAvailableTime(int serviceId, int year, int month, int day)
            => _medikTappApi.GetAppointmentAvailableTime(serviceId, year, month, day).ConfigureAwait(false);

        public ConfiguredTaskAwaitable<IEnumerable<Models.ScheduleModel>> GetAppointmentsByPatientId(int patientId)
            => _medikTappApi.GetAppointmentsByPatientId(patientId).ConfigureAwait(false);
    }
}