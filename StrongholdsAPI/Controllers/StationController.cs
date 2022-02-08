using Microsoft.AspNetCore.Mvc;
using StrongholdsUtil.Models;
using StrongholdsAPI.Managers;

namespace StrongholdsAPI.Controllers
{
    [ApiController]
    //[Route("/my/Robots")]
    public class StationController : ControllerBase
    {
        private readonly StationManager _repo;
        private readonly ILogger<StationController> _logger;

        public StationController(ILogger<StationController> logger, StationManager repo)
        {
            _logger = logger;
            _repo = repo;
        }
        
        // Returns a list of all stations
        [HttpGet, Route("/Stations/")]
        public IEnumerable<Station> GetAll()
        {
            return _repo.GetAll();
        }

        // Returns a list of all stations belonging to a user
        [HttpGet, Route("/my/Stations/{id}")]
        public Station Get(int id, string token)
        {
            return _repo.Get(id, token);
        }

        //Returns a specific station belonging to a user, by stationID
        [HttpGet, Route("/my/Stations/")]
        public Station Get(string token)
        {
            return _repo.Get(token);
        }
    }


}