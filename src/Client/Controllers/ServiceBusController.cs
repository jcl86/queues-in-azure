using Microsoft.AspNetCore.Mvc;
using Client.Models;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Controllers
{
    public class ServiceBusController : Controller
    {
        private readonly ServiceBusQueueSender queueSender;

        public ServiceBusController(ServiceBusQueueSender queueSender)
        {
            this.queueSender = queueSender;
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(MessageModel message)
        {
            await queueSender.Send(message.Content);
            ViewBag.Message = "Message was sent correctly";
            return View();
        }

    }
}
