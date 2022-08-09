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

        [HttpGet, Route("/")]
        public ContentResult Index()
        {
            var html = System.IO.File.ReadAllText(@"./Static/Index.html");
        
            return base.Content(html, "text/html");
        }

        // Returns a list of all stations
        [HttpGet, Route("/Status/")]
        public string GetStatus()
        {
            return "Strongholds is online";
        }

    }
}