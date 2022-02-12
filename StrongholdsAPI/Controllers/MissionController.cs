using Microsoft.AspNetCore.Mvc;
using StrongholdsUtil.Models;
using StrongholdsAPI.Managers;
using Newtonsoft.Json;

namespace StrongholdsAPI.Controllers
{
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly MissionManager _repo;
        private readonly ILogger<MissionController> _logger;

        public MissionController(ILogger<MissionController> logger, MissionManager repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // Create a new mission for a Robot
        [HttpPost, Route("/Missions/new/{missionContent}")]
        public void NewMission(string missionContent)
        {
            var mission = JsonConvert.DeserializeObject<NewMission>(missionContent);
            var n = mission.Mission.Objectives.Count;

            var missionID = _repo.AddMission(mission.Mission);

            //foreach (Objective o in mission.Mission.Objectives)
            //{
            //    o.MissionID = missionID;
            //    _repo.AddObjective(o);
            //}
        }

    }
}