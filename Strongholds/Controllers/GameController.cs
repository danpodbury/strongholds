using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

using Strongholds.Models;
using Strongholds.ViewModels;
using Strongholds.Filters;
using StrongholdsUtil.Models;

namespace Strongholds.Controllers
{
    //[AuthorizeUser]
    public class GameController : Controller
    {
        private readonly ILogger<GameController> _logger;
        private APIUtils API;

        public GameController(ILogger<GameController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            API = new APIUtils(clientFactory);
            
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");
            var result = await API.GetResultFromAsync($"/my/Robots/?token={token}");
            
            List<Robot> robots = JsonConvert.DeserializeObject<List<Robot>>(result);

            var model = new GameView()
            {
                MyRobots = robots
            };
            return View(model);
        }

        public async Task<IActionResult> NewMission(int robotID)
        {
            var model = new NewMission
            {
                RobotID = robotID,
                Mission = new Mission()
                {
                    Objectives = new List<Objective>(new Objective[10])
                }
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewMission(NewMission model)
        {
            var objs = model.Mission.Objectives;
            if (!ModelState.IsValid)
            {
                return View(nameof(NewMission));
            }

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