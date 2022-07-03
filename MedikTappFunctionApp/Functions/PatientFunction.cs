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
    public class PatientFunction : BaseFunction
    {
        public PatientFunction(EntityContext entityContext, JsonService jsonService)
            : base(entityContext, jsonService) { }

        [FunctionName("Register")]
        public async Task<IActionResult> Register([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                await EntityContext.PatientData.AddAsync(JsonService.ReadJsonRequestMessage<PatientModel>(request.Body));
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Registered new patient for MedikTapp");

                return new OkObjectResult(await EntityContext.PatientData.AsNoTracking()
                    .OrderByDescending(x => x.PatientId).FirstOrDefaultAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Register function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }

        [FunctionName("Login")]
        public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                // Get the email and password sent by client
                var requestData = JsonService.ReadJsonRequestMessage<PatientModel>(request.Body);
                var patient = await EntityContext.PatientData.AsNoTracking()
                    .FirstAsync(x => x.Email == requestData.Email && x.Password == requestData.Password);

                return patient.PatientId == 0
                    ? new NotFoundObjectResult("Patient is not found on MedikTapp's database")
                    : new OkObjectResult(patient);
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Login function, see the returned response for more information: ", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
            }
        }
    }
}