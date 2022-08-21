using System.Text;
using Microsoft.AspNetCore.Mvc;
using Strongholds.Models;
using StrongholdsUtil.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using Strongholds.ViewModels;

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
            int loginID = JsonConvert.DeserializeObject<Int32>(result);

            if (loginID == -1)
            {
                ModelState.AddModelError("Login", "Login not valid or doesn't exist");
                return View(nameof(Login));
            }

            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetInt32("loginID", loginID);

            ModelState.ClearValidationState("Login");

            return RedirectToAction(nameof(Index));
        }

        // Sign Up
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Claim(SignUpView model)
        {
            if (ModelState.IsValid)
            {
                //TODO: is this potentially vulnerable to injection?
                var response = await API.PostQueryString($"SignUp/{model.Username}", model.Username);
        
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    Login l = JsonConvert.DeserializeObject<Login>(result);

                    if (l.Error != null)
                    {
                        ModelState.AddModelError("Username", l.Error.ErrorText);
                        return View(nameof(SignUp), model);
                    }

                    return View("SignUpResult", l);
                }
 
            }
        
            return RedirectToAction(nameof(SignUp));
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("loginID");
            return RedirectToAction(nameof(Index));
        }

        // Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}