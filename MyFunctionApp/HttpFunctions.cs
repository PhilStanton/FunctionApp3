using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Durables
{
    public static class HttpFunctions
    {
        [Function(nameof(MyHttpTrigger))]
        public static async Task<HttpResponseData> MyHttpTrigger(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
           [DurableClient] DurableTaskClient client,
           FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(MyHttpTrigger));

            // Function input comes from the request content.
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync("MyOrchestrator");

            logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

            // Returns an HTTP 202 response with an instance management payload.
            // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
            
            return client.CreateCheckStatusResponse(req, instanceId);                            
        }        
    }
}
