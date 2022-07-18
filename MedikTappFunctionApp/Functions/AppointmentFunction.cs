using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTappFunctionApp.Functions
{
    public class AppointmentFunction : BaseFunction
    {
        public AppointmentFunction(EntityContext entityContext, JsonService jsonService)
            : base(entityContext, jsonService) { }

        #region Admin
        [FunctionName("GetAppointments")]
        public async Task<IActionResult> GetAppointments([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            // TODO: Appointment with service picker and calendar control
            try
            {
                var year = int.Parse(request.Query["year"]);
                var month = int.Parse(request.Query["month"]);
                var day = int.Parse(request.Query["day"]);
                var serviceId = int.Parse(request.Query["serviceId"]);
                var completeDateTime = new DateTime(year, month, day);

                var appointments = await EntityContext.AppointmentData.AsNoTracking().ToListAsync();
                var patients = await EntityContext.PatientData.AsNoTracking().ToListAsync();
                var filteredAppointments = appointments.Where(x => x.AppointmentDate.Date == completeDateTime.Date && x.ServiceId == serviceId);

                var filteredData = filteredAppointments.Join(patients,
                    appointments => appointments.PatientId,
                    patients => patients.PatientId,
                    (appointments, patients) => new
                    {
                        appointments.AppointmentDate,
                        patientFullName = $"{patients.FirstName} {patients.LastName}",
                        appointments.InBehalf,
                        prospectFullName = $"{appointments.ProspectFirstName} {appointments.ProspectLastName}",
                    }).ToList();

                return new OkObjectResult(filteredData.ToList());
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "GetAppointments");
            }
        }
        #endregion

        #region Patient
        [FunctionName("AddAppointment")]
        public async Task<IActionResult> AddAppointment([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                var appointmentTable = EntityContext.AppointmentData;
                await appointmentTable.AddAsync(JsonService.ReadJsonRequestMessage<AppointmentModel>(request.Body));
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Inserted new service in the database");

                var lastAppointmentId = await appointmentTable.AsNoTracking().OrderBy(_ => _.AppointmentId).LastAsync();
                return new OkObjectResult(lastAppointmentId.AppointmentId);
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "AddAppointment");
            }
        }

        [FunctionName("CancelAppointment")]
        public async Task<IActionResult> CancelAppointment([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest request, ILogger logger)
        {
            try
            {
                var appointmentId = int.Parse(request.Query["appointmentId"]);
                var matchingAppointment = await EntityContext.AppointmentData.FirstAsync(x => x.AppointmentId == appointmentId);
                if (matchingAppointment != null)
                {
                    EntityContext.AppointmentData.Remove(matchingAppointment);
                    await EntityContext.SaveChangesAsync();
                }

                logger.LogInformation($"Cancelled appointment with an appointment id of {appointmentId} in the database");
                return new OkObjectResult($"Succesfully cancelled an appointment id of {appointmentId} in the database");
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "CancelAppointment");
            }
        }

        [FunctionName("GetServiceAppointmentAvailableTimes")]
        public async Task<IActionResult> GetServiceAppointmentAvailableTimes([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all service's available time based on selected date and time");
                var serviceId = int.Parse(request.Query["serviceId"]);
                var year = int.Parse(request.Query["year"]);
                var month = int.Parse(request.Query["month"]);
                var day = int.Parse(request.Query["day"]);

                var requestedDate = new DateTime(year, month, day);
                var appointmentsTable = EntityContext.AppointmentData.AsNoTracking();
                var availableDateAndTime = new List<DateTime>();

                for (var i = 0; i < 10; i++)
                    // Clinic hours: 7 am to 4pm
                    availableDateAndTime.Add(new DateTime(year, month, day, i + 7, 0, 0));

                var dbTimesToRemove = await appointmentsTable.Where(x => x.AppointmentDate.Date == requestedDate.Date
                        && x.ServiceId == serviceId).ToListAsync();

                if (requestedDate == DateTime.Now.Date)
                {
                    // Selected date is today
                    var localTimesToRemove = availableDateAndTime.Where(x => x.Hour < DateTime.Now.Hour + 2).ToList();
                    foreach (var item in localTimesToRemove)
                        availableDateAndTime.Remove(item);

                    foreach (var dbItem in dbTimesToRemove)
                        availableDateAndTime.Remove(dbItem.AppointmentDate);
                }
                else
                    // Selected date is in the future
                    foreach (var dbItem in dbTimesToRemove)
                        availableDateAndTime.Remove(dbItem.AppointmentDate);

                return new OkObjectResult(availableDateAndTime);
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "GetServiceAppointmentAvailableTimes");
            }
        }

        [FunctionName("GetAppointmentsByPatientId")]
        public async Task<IActionResult> GetAppointmentsByPatientId([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            // Appointments tab in MedikTapp
            try
            {
                logger.LogInformation("Returning all appointments by patient's id");
                var patientAppointments = EntityContext.AppointmentData.Where(x => x.PatientId == int.Parse(request.Query["patientId"])).AsNoTracking();
                var servicesTable = EntityContext.ServiceData.AsNoTracking();

                var getEachAppointmentDetails = patientAppointments.Join(servicesTable,
                    appointment => appointment.ServiceId,
                    service => service.ServiceId,
                    (appointment, service) => new
                    {
                        service.ServiceId,
                        service.ServiceName,
                        service.ServiceDescription,
                        service.ServiceImage,
                        appointment.AppointmentId,
                        appointment.AppointmentDate,
                        appointment.BookingStatus,
                        appointment.ProspectFirstName,
                        appointment.ProspectLastName
                    });

                return new OkObjectResult(await getEachAppointmentDetails.ToListAsync());
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "GetAppointmentsByPatientId");
            }
        }

        [FunctionName("RescheduleAppointment")]
        public async Task<IActionResult> RescheduleAppointment([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest request, ILogger logger)
        {
            try
            {
                var servicePayload = JsonService.ReadJsonRequestMessage<AppointmentModel>(request.Body);
                var matchingAppointment = await EntityContext.AppointmentData.FirstAsync(x => x.AppointmentId == servicePayload.AppointmentId);
                if (matchingAppointment != null)
                {
                    matchingAppointment.AppointmentDate = servicePayload.AppointmentDate;
                    matchingAppointment.BookingStatus = servicePayload.BookingStatus;
                    matchingAppointment.InBehalf = servicePayload.InBehalf;
                    matchingAppointment.ProspectFirstName = servicePayload.ProspectFirstName;
                    matchingAppointment.ProspectLastName = servicePayload.ProspectLastName;
                    matchingAppointment.ProspectAge = servicePayload.ProspectAge;
                    matchingAppointment.ProspectGender = servicePayload.ProspectGender;

                    await EntityContext.SaveChangesAsync();
                }

                logger.LogInformation($"Edited an appointment with an appointment id of {matchingAppointment.AppointmentId} in the database");
                return new OkObjectResult(matchingAppointment.AppointmentId);

            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "CancelAppointment");
            }
        }
        #endregion
    }
}