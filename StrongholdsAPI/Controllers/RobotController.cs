using Microsoft.AspNetCore.Mvc;
using StrongholdsUtil.Models;
using StrongholdsAPI.Managers;

namespace StrongholdsAPI.Controllers
{
    [ApiController]
    //[Route("/my/Robots")]
    public class RobotController : ControllerBase
    {
        private readonly RobotManager _repo;
        private readonly ILogger<RobotController> _logger;

        public RobotController(ILogger<RobotController> logger, RobotManager repo)
        {
            _logger = logger;
            _repo = repo;
        }
        
        // Returns a json list of all robots 
        [HttpGet, Route("/my/Robots/")]
        public IEnumerable<Robot> Get(string token)
        {
            return _repo.Get(token);
        }

        // Returns a single robot by id
        [HttpGet, Route("/my/Robots/{id}")]
        public Robot Get(int id, string token)
        {
            return _repo.Get(id, token);
        }


    }


}