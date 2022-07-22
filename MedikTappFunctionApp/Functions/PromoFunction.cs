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
    public class PromoFunction : BaseFunction
    {
        public PromoFunction(EntityContext entityContext, JsonService jsonService)
            : base(entityContext, jsonService) { }

        [FunctionName("GetPromo")]
        public async Task<IActionResult> GetPromo([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest request, ILogger logger)
        {
            try
            {
                logger.LogInformation("Returning all promos");
                return new OkObjectResult(await EntityContext.ServiceData.Where(_ => _.ServiceName.StartsWith("(Promo)"))
                    .AsNoTracking().ToListAsync());
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "GetPromo");
            }
        }

        [FunctionName("AddPromo")]
        public async Task<IActionResult> AddPromo([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                var serviceTable = EntityContext.ServiceData;
                var serviceRequest = JsonService.ReadJsonRequestMessage<ServiceModel>(request.Body);

                await serviceTable.AddAsync(serviceRequest);
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Inserted new service in the database");

                var lastAppointmentId = await serviceTable.AsNoTracking().OrderBy(_ => _.ServiceId).LastAsync();
                return new OkObjectResult(lastAppointmentId.ServiceId);
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "AddPromo");
            }
        }

        [FunctionName("EditPromo")]
        public async Task<IActionResult> EditPromo([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest request, ILogger logger)
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
                return ExceptionHelper(ex, logger, "EditPromo");
            }
        }

        [FunctionName("DeletePromo")]
        public async Task<IActionResult> DeletePromo([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest request, ILogger logger)
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
                return ExceptionHelper(ex, logger, "DeletePromo");
            }
        }
    }
}