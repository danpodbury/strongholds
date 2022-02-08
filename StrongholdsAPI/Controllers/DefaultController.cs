using Microsoft.AspNetCore.Mvc;

namespace StrongholdsAPI.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }
        
        // Returns a list of all stations
        [HttpGet, Route("/Status/")]
        public string GetStatus()
        {
            return "Strongholds is online";
        }

    }
}