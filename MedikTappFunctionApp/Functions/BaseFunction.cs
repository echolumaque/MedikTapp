using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace MedikTappFunctionApp.Functions
{
    public class BaseFunction
    {
        protected EntityContext EntityContext { get; private set; }
        protected JsonService JsonService { get; private set; }

        public BaseFunction(EntityContext entityContext, JsonService jsonService)
        {
            EntityContext = entityContext;
            JsonService = jsonService;
        }

        protected BadRequestObjectResult ExceptionHelper(Exception ex, ILogger logger, string functionName)
        {
            logger.LogError($"A problem happened in {functionName} function, see the returned response for more information: ",
                JsonConvert.SerializeObject(ex, Formatting.Indented));
            return new BadRequestObjectResult(JsonConvert.SerializeObject(ex, Formatting.Indented));
        }
    }
}