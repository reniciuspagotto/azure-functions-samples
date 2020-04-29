using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace AzureFunctionTimeTrigger
{
    public static class GetProductAndSync
    {
        [FunctionName("GetProductAndSync")]
        public static void Run([TimerTrigger("5 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
