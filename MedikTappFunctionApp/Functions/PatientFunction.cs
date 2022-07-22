using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MedikTappFunctionApp.Functions
{
    public class PatientFunction : BaseFunction
    {
        public PatientFunction(EntityContext entityContext, JsonService jsonService)
            : base(entityContext, jsonService) { }

        [FunctionName("ChangePassword")]
        public async Task<IActionResult> ChangePassword([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequest request, ILogger logger)
        {
            try
            {
                var patient = await EntityContext.PatientData.FirstOrDefaultAsync(_ => _.PatientId == int.Parse(request.Query["patientId"]));
                patient.Password = request.Query["newPassword"];
                await EntityContext.SaveChangesAsync();
                return new OkObjectResult("Password succesfully changed");
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "ChangePassword");
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
                    .FirstOrDefaultAsync(_ => _.Username == requestData.Username && _.Password == requestData.Password);

                return patient == null
                    ? new NotFoundObjectResult("Patient is not found on MedikTapp's database")
                    : new OkObjectResult(patient);
            }
            catch (Exception ex)
            {
                return ExceptionHelper(ex, logger, "Login");
            }
        }

        [FunctionName("Register")]
        public async Task<IActionResult> Register([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest request, ILogger logger)
        {
            try
            {
                await EntityContext.PatientData.AddAsync(JsonService.ReadJsonRequestMessage<PatientModel>(request.Body));
                await EntityContext.SaveChangesAsync();

                logger.LogInformation("Registered new patient for MedikTapp");
                return new OkObjectResult("Registered new patient for MedikTapp");
            }
            catch (Exception ex)
            {
                return ex.GetType() == typeof(DbUpdateException)
                    ? new ConflictObjectResult("Patient already exists MedikTapp's database")
                    : ExceptionHelper(ex, logger, "Register");
            }
        }
    }
}