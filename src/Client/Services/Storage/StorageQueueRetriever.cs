using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Client.Services
{
    public class StorageQueueRetriever
    {
        public const string QueueName = "cola";

        private readonly string connectionString;

        public StorageQueueRetriever(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("AzureStorage:ConnectionString").Value;
        }

        public async Task<QueueClient> GetQueue()
        {
            QueueClient client = new QueueClient(connectionString, QueueName);

            await client.CreateIfNotExistsAsync();

            return client;
        }
    }
}
