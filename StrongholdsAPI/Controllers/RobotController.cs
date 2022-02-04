using Microsoft.AspNetCore.Mvc;
using StrongholdsAPI.Models;
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
        public IEnumerable<Robot> Get()
        {
            return _repo.Get();
        }

        // Returns a single robot by id
        [HttpGet, Route("/my/Robots/{id}")]
        public Robot Get(int id)
        {
            return _repo.Get(id);
        }
    }


}