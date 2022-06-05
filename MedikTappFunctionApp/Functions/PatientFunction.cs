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
using System.Threading.Tasks;

namespace MedikTappFunctionApp.Functions
{
    public class PatientFunction : BaseFunction
    {
        private readonly SqlService _sqlService;

        public PatientFunction(EntityContext entityContext, JsonService jsonService, SqlService sqlService)
            : base(entityContext, jsonService) => _sqlService = sqlService;

        [FunctionName("Register")]
        public async Task<IActionResult> Register([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                await EntityContext.PatientData.AddAsync(JsonService.ReadJsonRequestMessage<PatientModel>(request.Body));
                await EntityContext.SaveChangesAsync();
                logger.LogInformation("Registered new patient for MedikTapp");

                return new OkObjectResult("Succesfully registered a new patient");
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Register function, see the returned response for more information: ", JsonConvert.SerializeObject(ex));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex));
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
                    : new OkObjectResult(new
                    {
                        patient.PatientId,
                        patient.Name
                    });
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Login function, see the returned response for more information: ", JsonConvert.SerializeObject(ex));
                return new BadRequestObjectResult(JsonConvert.SerializeObject(ex));
            }
        }
    }
}