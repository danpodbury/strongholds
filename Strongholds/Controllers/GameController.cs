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
    [AuthorizeUser]
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
            var name = HttpContext.Session.GetString("username");
            var loginID = HttpContext.Session.GetInt32("loginID");

            //Get user login
            //var result = await API.GetResultFromAsync($"/my/Account/?token={token}&loginID={loginID}");
            //Login login = JsonConvert.DeserializeObject<Login>(result);

            //Get user robots
            var result = await API.GetResultFromAsync($"/my/Robots/?token={token}");
            List<Robot> robots = JsonConvert.DeserializeObject<List<Robot>>(result);

            ////Get user stations
            //result = await API.GetResultFromAsync($"/my/Stations/{id}");
            //List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(result);

            //Get user missions
            var result2 = await API.GetResultFromAsync($"/my/Missions/{loginID}?token={token}");
            List<Mission> missions= JsonConvert.DeserializeObject<List<Mission>>(result2);

            var model = new GameView()
            {
                MyRobots = robots,
                MyMissions = missions,
                //MyStations = stations,
                //MyLogin = login
            };
            return View(model);
        }

        public async Task<IActionResult> NewMission(int robotID)
        {
            // Verifiy user owns this robotID
            var token = HttpContext.Session.GetString("token");
            var authorized = await API.CanGetSuccessfully($"/my/Robots/{robotID}/?token={token}");

            if (!authorized)
            {
                return Json("That robot does not respond to you.");
            } else
            {
                var result2 = await API.GetResultFromAsync($"/my/Robots/{robotID}/Missions/?token={token}");
                List<Mission> missions = JsonConvert.DeserializeObject<List<Mission>>(result2);

                if (missions.Count > 0)
                {
                    return Json("That robot is already on a mission.");
                }
            }

            var model = new Mission()
            {
                RobotID = robotID,
                Objectives = new List<Objective>(new Objective[10])
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewMission(Mission model)
        {
            // Verifiy user owns this robotID
            var token = HttpContext.Session.GetString("token");
            var authorized = await API.CanGetSuccessfully($"/my/Robots/{model.RobotID}/?token={token}");
            if (!authorized) return Json("That robot does not respond to you.");

            // TODO: fix model state validation
            //if (!ModelState.IsValid)
            //{
            //    return View(nameof(NewMission));
            //}

            var content = JsonConvert.SerializeObject(model);

            // Send post to API
            var response = await API.PostQueryString($"/Missions/new/{content}", content);

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