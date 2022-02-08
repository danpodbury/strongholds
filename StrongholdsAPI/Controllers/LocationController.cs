using Microsoft.AspNetCore.Mvc;
using StrongholdsUtil.Models;
using StrongholdsAPI.Managers;

namespace StrongholdsAPI.Controllers
{
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationManager _repo;

        private readonly ILogger<LocationController> _logger;

        public LocationController(ILogger<LocationController> logger, LocationManager repo)
        {
            _logger = logger;
            _repo = repo;
        }

        
        // Returns a json list of all robots 
        [HttpGet, Route("/Location/")]
        public IEnumerable<Location> Get()
        {
            return _repo.Get();
        }

        // Returns a single robot by id
        [HttpGet, Route("/Location/{id}")]
        public Location Get(int id)
        {
            return _repo.Get(id);
        }
    }
}