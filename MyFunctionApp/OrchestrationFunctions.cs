using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

namespace Durables
{
    public class OrchestrationFunctions
    {
        [Function(nameof(MyOrchestrator))]
        public async Task<List<string>> MyOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(MyOrchestrator));
            logger.LogInformation("Saying hello.");
            var outputs = new List<string>();

            // Replace name and input with values relevant for your Durable Functions Activity
            outputs.Add(await context.CallActivityAsync<string>(nameof(ActivityFunctions.SayHello), "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(ActivityFunctions.SayHello), "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(ActivityFunctions.SayHello), "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }    
    }
}
