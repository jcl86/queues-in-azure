using System.Threading.Tasks;

namespace Client.Services
{
    public class StorageQueueSender
    {
        private readonly StorageQueueRetriever queueRetriever;

        public StorageQueueSender(StorageQueueRetriever queueRetriever)
        {
            this.queueRetriever = queueRetriever;
        }

        public async Task Send(string messageBody)
        {
            var queue = await queueRetriever.GetQueue();

            await queue.SendMessageAsync(messageBody);
        }
    }    

}
