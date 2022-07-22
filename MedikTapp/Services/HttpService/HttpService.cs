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

        public ConfiguredTaskAwaitable<int> AddAppointment(AddAppointmentModel appointment)
        {
            return _medikTappApi.AddAppointment(appointment).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable CancelAppointment(int appointmentId)
        {
            return _medikTappApi.CancelAppointment(appointmentId).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable ChangePassword(int patientId, string newPassword)
        {
            return _medikTappApi.ChangePassword(patientId, newPassword).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<DateTime>> GetAppointmentAvailableTime(int serviceId, int year, int month, int day)
        {
            return _medikTappApi.GetAppointmentAvailableTime(serviceId, year, month, day).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<AppointmentModel>> GetPatientCancelledAppointment(int patientId)
        {
            return _medikTappApi.GetPatientCancelledAppointment(patientId).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<AppointmentModel>> GetPatientCompletedAppointment(int patientId)
        {
            return _medikTappApi.GetPatientCompletedAppointment(patientId).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<AppointmentModel>> GetPatientUpcomingAppointment(int patientId)
        {
            return _medikTappApi.GetPatientUpcomingAppointment(patientId).ConfigureAwait(false);
        }
        public ConfiguredTaskAwaitable<IEnumerable<Models.Services>> GetPromos()
        {
            return _medikTappApi.GetPromos().ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<Models.Services>> GetServices()
        {
            return _medikTappApi.GetService().ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<PatientModel> Login(PatientModel patient)
        {
            return _medikTappApi.Login(patient).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable Register(PatientModel patient)
        {
            return _medikTappApi.Register(patient).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<int> RescheduleAppointment(AddAppointmentModel appointment)
        {
            return _medikTappApi.RescheduleAppointment(appointment).ConfigureAwait(false);
        }
    }
}