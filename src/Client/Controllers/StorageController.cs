﻿using Microsoft.AspNetCore.Mvc;
using Client.Models;
using System.Threading.Tasks;
using Client.Services;

namespace Client.Controllers
{
    public class StorageController : Controller
    {
        private readonly StorageQueueSender sender;
        private readonly StorageQueueReceiver receiver;

        public StorageController(StorageQueueSender sender, StorageQueueReceiver receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(MessageModel model)
        {
            await sender.Send(model);
            ViewBag.Message = "Message was sent correctly";
            return View();
        }

        public IActionResult Receive()
        {
            return View(new MessageListModel());
        }

        [HttpPost]
        public async Task<IActionResult> Receive(MessageListModel model)
        {
            var message = await receiver.Receive();
            if (message != null && !string.IsNullOrWhiteSpace(message.Content))
            {
                if (model.Messages is null)
                {
                    model.Messages = new System.Collections.Generic.List<string>();
                }
                model.Messages.Add(message.Content);
                ViewBag.Message = $"A new message was received: {message.Content}";
            }
            else ViewBag.Message = "There are no messages in the queue";

            return View(model);
        }
    }
}
