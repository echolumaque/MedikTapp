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
                return new OkObjectResult(await EntityContext.PromoData.OrderBy(x => x.PromoId).AsNoTracking().ToListAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in GetPromo function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
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
                logger.LogError($"A problem happened in  AddPromo, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("EditPromo")]
        public async Task<IActionResult> EditPromo([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                var promoPayload = JsonService.ReadJsonRequestMessage<PromoModel>(request.Body);
                var matchingService = await EntityContext.PromoData.FirstAsync(x => x.PromoId == promoPayload.PromoId);
                if (matchingService != null)
                {
                    matchingService.PromoPrice = promoPayload.PromoPrice;
                    matchingService.PromoImage = promoPayload.PromoImage;
                    matchingService.PromoName = promoPayload.PromoName;
                    matchingService.PromoDescription = promoPayload.PromoDescription;

                    await EntityContext.SaveChangesAsync();
                }

                logger.LogInformation($"Edited {promoPayload.PromoName} in the database");
                return new OkObjectResult($"Succesfully edited {promoPayload.PromoName} in the database");
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in EditPromo, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("DeletePromo")]
        public async Task<IActionResult> DeletePromo([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest request, ILogger logger)
        {
            try
            {
                var promoId = int.Parse(request.Query["promoId"]);
                var matchingPromo = await EntityContext.PromoData.FirstAsync(x => x.PromoId == promoId);
                if (matchingPromo != null)
                {
                    EntityContext.PromoData.Remove(matchingPromo);
                    await EntityContext.SaveChangesAsync();
                }

                logger.LogInformation($"Deleted {matchingPromo.PromoName} in the database");
                return new OkObjectResult($"Succesfully Deleted {matchingPromo.PromoName} in the database");
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in DeletePromo, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }
    }
}