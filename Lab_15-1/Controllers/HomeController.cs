using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lab_15_1.Models;
using System.Net.Http;

namespace Lab_15_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> ViewCards()
        {
            //1. Make the HttpClient
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; GrandCircus/1.0)");
            //2. Make the API call and put the result in a variable
            var response = await client.GetAsync("api/deck/new/draw/?count=5");
            //3. Parse the response contents as your typed object
            //in this case, it contains an array of JokeContent objects inside the Value
            var deck = await response.Content.ReadAsAsync<Deck>();
            return View(deck);
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
