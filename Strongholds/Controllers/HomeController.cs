﻿using System.Text;
using Microsoft.AspNetCore.Mvc;
using Strongholds.Models;
using StrongholdsUtil.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Strongholds.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _clientFactory;
        private HttpClient Client => _clientFactory.CreateClient();
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string failureString = "Cannot reach server!";
            string result = await GetResultFromAsync($"/Status/", failureString);

            string classAppearance = (failureString != result)? "success" : "danger";

            return View(nameof(Index), new Tuple<string, string>(classAppearance, result) );
        }
       
        

        public async Task<IActionResult> SignUp()
        {
            return View();
        }


        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Claim(string username)
        {
            if (ModelState.IsValid)
            {
                StringContent content = new StringContent(nameof(username) +"="+ username);
                var postResponse = Client.PostAsync($"SignUp/{username}", content).Result;
        
                if (postResponse.IsSuccessStatusCode)
                {
                    var result = await postResponse.Content.ReadAsStringAsync();
                    JsonConvert.DeserializeObject<Login>(result);

                    return RedirectToAction(nameof(Index));
                }
 
            }
        
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> GetResultFromAsync(string url, string failureString)
        {
            try
            {
                var response = await Client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return "request was not successful";

                // Deserializing the response received from web api and storing into a list.
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return failureString;
            }

        }
    }
}