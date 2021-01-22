﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using Client.Services;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiceBusQueueSender queueSender;

        public HomeController(ServiceBusQueueSender queueSender)
        {
            this.queueSender = queueSender;
        }


        public IActionResult Index()
        {
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
