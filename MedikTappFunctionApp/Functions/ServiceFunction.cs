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
using System.Linq;
using System.Threading.Tasks;

namespace MedikTappFunctionApp.Functions
{
    public class ServiceFunction : BaseFunction
    {
        public ServiceFunction(EntityContext entityContext, JsonService jsonService)
            : base(entityContext, jsonService) { }

        [FunctionName("GetService")]
        public async Task<IActionResult> GetService([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all services");
                return new OkObjectResult(await EntityContext.ServiceData.OrderBy(x => x.ServiceId).AsNoTracking().ToListAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetService function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("GetServiceNameAndId")]
        public async Task<IActionResult> GetAppointmentsByServiceId([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all appointments by service");
                var services = await EntityContext.ServiceData.AsNoTracking().ToListAsync();

                return new OkObjectResult(services.Select(x => new
                {
                    x.ServiceId,
                    x.ServiceName
                }));
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetAppointmentsByServiceId function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("AddService")]
        public async Task<IActionResult> AddService([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                await EntityContext.ServiceData.AddAsync(JsonService.ReadJsonRequestMessage<ServiceModel>(request.Body));
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Inserted new service in the database");

                return new OkObjectResult("Succesfully inserted new service in the database");
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Login AddService, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("AddPromo")]
        public async Task<IActionResult> AddPromo([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                await EntityContext.PromoData.AddAsync(JsonService.ReadJsonRequestMessage<PromoModel>(request.Body));
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Inserted new promo in the database");

                return new OkObjectResult("Succesfully inserted new promo in the database");
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Login AddPromo, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }
    }
}