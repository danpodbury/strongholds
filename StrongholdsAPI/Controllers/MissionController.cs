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
        public int NewMission(string missionContent)
        {
            var mission = JsonConvert.DeserializeObject<Mission>(missionContent);
            return _repo.AddMission(mission);
        }

        // Get all the missions belonging to a user's robots
        [HttpGet, Route("/my/Missions/{loginID}")]
        public List<Mission> GetUserMissions(int loginID, string token)
        {
            return _repo.GetMissionsByLoginID(loginID);
        }

        // Get a robots current mission/pending missions
        [HttpGet, Route("/my/Robots/{robotID}/Missions/")]
        public List<Mission> GetRobotMission(int robotID, string token)
        {
            return _repo.GetMissionsByRobotID(robotID);
        }

    }
}