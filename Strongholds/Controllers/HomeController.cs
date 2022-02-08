using System.Text;
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
        private APIUtils API;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            API = new APIUtils(clientFactory);
        }

        // HomePage
        public async Task<IActionResult> Index()
        {
            string failureString = "Cannot reach server!";
            string result = await API.GetResultFromAsync($"/Status/", failureString);

            string classAppearance = (failureString != result)? "success" : "danger";

            return View(nameof(Index), new Tuple<string, string>(classAppearance, result) );
        }


        // Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(string username, string token)
        {
            var result = await API.GetResultFromAsync($"/Login/?username={username}&token={token}");
            bool loginValid = JsonConvert.DeserializeObject<Boolean>(result);

            if (!loginValid)
            {
                ModelState.AddModelError("Login", "Login not valid or doesn't exist");
                return View(nameof(Login));
            }

            HttpContext.Session.SetString("token", token);

            ModelState.ClearValidationState("Login");

            return RedirectToAction(nameof(Index));
        }

        // Sign Up
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Claim(string username)
        {
            if (ModelState.IsValid)
            {
                var response = await API.PostQueryString($"SignUp/{username}", username);
        
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    Login l = JsonConvert.DeserializeObject<Login>(result);

                    return View("SignUpResult", l);
                }
 
            }
        
            return RedirectToAction(nameof(SignUp));
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("token");
            return View(nameof(Index));
        }

        // Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}