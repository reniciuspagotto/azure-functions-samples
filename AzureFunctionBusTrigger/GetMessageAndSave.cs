using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureFunctionBusTrigger
{
    public static class GetMessageAndSave
    {
        [FunctionName("GetMessageAndSave")]
        public static void Run([ServiceBusTrigger("product", Connection = "AzureServiceBusConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
