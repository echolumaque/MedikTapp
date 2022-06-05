using MedikTappFunctionApp.Models;
using MedikTappFunctionApp.Services;

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
    }
}