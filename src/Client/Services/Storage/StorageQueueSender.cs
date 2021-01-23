using Client.Models;
using Newtonsoft.Json;
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

        public async Task Send(MessageModel message)
        {
            var queue = await queueRetriever.GetQueue();
            var json = JsonConvert.SerializeObject(message);
            await queue.SendMessageAsync(json);
        }
    }    

}
