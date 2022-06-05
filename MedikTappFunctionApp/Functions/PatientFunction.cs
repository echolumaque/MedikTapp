using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
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
                logger.LogError($"A problem happened in Register function, see the returned response for more information: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [FunctionName("Login")]
        public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                // Get the email and password sent by client
                var requestData = JsonService.ReadJsonRequestMessage<PatientModel>(request.Body);
                var result = await _sqlService.ExecuteStoredProcedure("Login",
                    ("@email", requestData.Email),
                    ("@pw", requestData.Password));

                int patientId = default;
                while (await result.ReadAsync())
                    patientId = (int)result["PatientId"];

                return patientId == 0
                    ? new NotFoundObjectResult("Patient's record not found.")
                    : new OkObjectResult(patientId);
            }
            catch (Exception ex)
            {
                logger.LogError($"A problem happened in Login function, see the returned response for more information: {ex.Message}");
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}