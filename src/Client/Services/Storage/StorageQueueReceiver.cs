using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Client.Models;
using System.Threading.Tasks;

namespace Client.Services
{
    public class StorageQueueReceiver
    {
        private readonly StorageQueueRetriever queueRetriever;

        public StorageQueueReceiver(StorageQueueRetriever queueRetriever)
        {
            this.queueRetriever = queueRetriever;
        }

        public async Task<MessageModel> Receive()
        {
            QueueClient queue = await queueRetriever.GetQueue();

            bool exists = await queue.ExistsAsync();
            if (exists)
            {
                var message = await queue.ReceiveMessageAsync();
                if (message.Value != null)
                {
                    var result = message.Value.Body.ToObjectFromJson<MessageModel>();
                    await queue.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);
                    return result;
                }
            }

            return null;
        }
    }

}
