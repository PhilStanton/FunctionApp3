using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Durables
{
    public class ActivityFunctions
    {
        [Function(nameof(SayHello))]
        public async Task<string> SayHello([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger(nameof(SayHello));
            logger.LogInformation("Saying hello to {name}.", name);
            return $"Hello {name}!";
        }
    }
}
