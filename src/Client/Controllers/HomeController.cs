using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly QueueSender queueSender;

        public HomeController(QueueSender queueSender)
        {
            this.queueSender = queueSender;
        }


        public IActionResult Home ()
        {
            return View();
        }
        public IActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(MessageModel message)
        {
            await queueSender.Send(message.Content);
            ViewBag.Message = "Message was sent correctly";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
