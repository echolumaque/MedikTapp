using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedikTappFunctionApp.Functions
{
    public class AppointmentFunction : BaseFunction
    {
        public AppointmentFunction(EntityContext entityContext, JsonService jsonService) : base(entityContext, jsonService) { }

        [FunctionName("GetAppointmentsByServiceId")]
        public async Task<IActionResult> GetAppointmentsByServiceId([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all appointments by service");

                return new OkObjectResult(await EntityContext.AppointmentData
                    .Where(x => x.ServiceId == int.Parse(request.Query["serviceId"])).AsNoTracking().ToListAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetAppointmentsByServiceId function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("GetAppointmentsByDate")]
        public async Task<IActionResult> GetAppointmentsByDate([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all appointments by date");

                var year = int.Parse(request.Query["year"]);
                var month = int.Parse(request.Query["month"]);
                var day = int.Parse(request.Query["day"]);
                var hour = int.Parse(request.Query["hour"]);
                var completeDateTime = new DateTime(year, month, day, hour, 0, 0);

                return new OkObjectResult(await EntityContext.AppointmentData
                    .Where(x => x.AppointmentDate == completeDateTime).AsNoTracking().ToListAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetAppointmentsByDate function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("GetAppointmentAvailableTime")]
        public IActionResult GetAppointmentAvailableTime([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all service's available time based on selected date and time");
                var serviceId = int.Parse(request.Query["serviceId"]);
                var year = int.Parse(request.Query["year"]);
                var month = int.Parse(request.Query["month"]);
                var day = int.Parse(request.Query["day"]);
                var availableDateAndTime = new List<DateTime>();
                for (var i = 0; i < 10; i++)
                    availableDateAndTime.Add(new DateTime(year, month, day, i + 7, 0, 0));

                var appointmentsBasedOnServiceId = EntityContext.AppointmentData.AsNoTracking()
                    .Where(x => x.ServiceId == serviceId).AsEnumerable();

                var daySelected = new DateTime(year, month, day, 0, 0, 0);
                var occupiedAppointments = appointmentsBasedOnServiceId
                    .Where(x => new DateTime(x.AppointmentDate.Year, x.AppointmentDate.Month, x.AppointmentDate.Day) == daySelected)
                    .Select(x => x.AppointmentDate)
                    .ToList();

                return new OkObjectResult(availableDateAndTime.Except(occupiedAppointments).ToList());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetAppointmentAvailableTime function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("GetAppointmentsByPatientId")]
        public async Task<IActionResult> GetAppointmentsByPatientId([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all appointments by patient's id");
                var patientAppointments = EntityContext.AppointmentData
                    .Where(x => x.PatientId == int.Parse(request.Query["patientId"])).AsNoTracking();
                var servicesTable = EntityContext.ServiceData.AsNoTracking();

                var getEachAppointmentDetails = patientAppointments.Join(servicesTable,
                    appointment => appointment.ServiceId,
                    services => services.ServiceId,
                    (appointment, services) => new
                    {
                        services.ServiceDescription,
                        services.ServiceImage,
                        services.ServiceName,
                        services.ServicePrice,
                        services.ServiceId,
                        appointment.BookingStatus,
                        appointment.AppointmentDate
                    });

                return new OkObjectResult(await getEachAppointmentDetails.ToListAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetAppointmentsByPatientId function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("AddAppointment")]
        public async Task<IActionResult> AddAppointment([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                await EntityContext.AppointmentData.AddAsync(JsonService.ReadJsonRequestMessage<AppointmentModel>(request.Body));
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Inserted new service in the database");

                return new OkObjectResult("Succesfully inserted new appointment in the database");
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Login AddAppointment, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }
    }
}