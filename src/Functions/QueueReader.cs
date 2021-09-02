using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Functions
{
    public static class QueueReader
    {
        [FunctionName("QueueReader")]
        public static void Run([QueueTrigger("myqueue-items", Connection = "cconnn")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
