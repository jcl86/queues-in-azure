using Client.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Services
{
    public class QueueSender
    {
        private readonly string connection;

        public QueueSender(IConfiguration configuration)
        {
            connection = configuration.GetSection("QueueConnection").Value;
        }

        public async Task Send(string messageBody)
        {
            var queueClient = new QueueClient(connection, "salesmessages");

            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await queueClient.SendAsync(message);
            await queueClient.CloseAsync();
        }
    }

  
}
