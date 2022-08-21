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
            if (System.IO.File.Exists(@"./Static/Index.html"))
            {
                var html = System.IO.File.ReadAllText(@"./Static/Index.html");
                return base.Content(html, "text/html");
            } else
            {
                return base.Content("Could not find API docs.", "text/html");
            }

        }

        // Returns a list of all stations
        [HttpGet, Route("/Status/")]
        public string GetStatus()
        {
            return "Strongholds is online";
        }

    }
}