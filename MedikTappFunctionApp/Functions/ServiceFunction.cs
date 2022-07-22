using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                return new OkObjectResult(await EntityContext.ServiceData.AsNoTracking()
                    .Where(_ => _.StartDate == null).ToListAsync());
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "GetService");
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
                return ExceptionHelper(ex, logger, "AddService");
            }
        }

        [FunctionName("EditService")]
        public async Task<IActionResult> EditService([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest request, ILogger logger)
        {
            try
            {
                var servicePayload = JsonService.ReadJsonRequestMessage<ServiceModel>(request.Body);
                var matchingService = await EntityContext.ServiceData.FirstAsync(_ => _.ServiceId == servicePayload.ServiceId);
                if (matchingService != null)
                {
                    matchingService.ServicePrice = servicePayload.ServicePrice;
                    matchingService.ServiceImage = servicePayload.ServiceImage;
                    matchingService.ServiceName = servicePayload.ServiceName;
                    matchingService.ServiceDescription = servicePayload.ServiceDescription;

                    await EntityContext.SaveChangesAsync();
                }

                logger.LogInformation($"Edited {servicePayload.ServiceName} in the database");
                return new OkObjectResult($"Succesfully edited {servicePayload.ServiceName} in the database");
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "EditService");
            }
        }

        [FunctionName("DeleteService")]
        public async Task<IActionResult> DeleteService([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest request, ILogger logger)
        {
            try
            {
                var serviceId = int.Parse(request.Query["serviceId"]);
                var matchingService = await EntityContext.ServiceData.FirstAsync(_ => _.ServiceId == serviceId);
                if (matchingService != null)
                {
                    EntityContext.ServiceData.Remove(matchingService);
                    await EntityContext.SaveChangesAsync();
                }

                logger.LogInformation($"Deleted {matchingService.ServiceName} in the database");
                return new OkObjectResult($"Succesfully Deleted {matchingService.ServiceName} in the database");
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "DeleteService");
            }
        }

        [FunctionName("GetServiceNameAndId")]
        public async Task<IActionResult> GetServiceNameAndId([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            // TODO: Used in appointments picker on admin side
            try
            {
                logger.LogInformation("Returning all appointments by service");
                var services = EntityContext.ServiceData.AsNoTracking();
                var servicesList = await services.ToListAsync();

                return new OkObjectResult(servicesList.Select(x => new
                {
                    x.ServiceId,
                    x.ServiceName
                }));
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "GetServiceNameAndId");
            }
        }
    }
}