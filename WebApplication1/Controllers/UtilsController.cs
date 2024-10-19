using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilsController : ControllerBase
    {
        private readonly ILogger<UtilsController> _logger;

        public UtilsController(ILogger<UtilsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Ip")]
        public IActionResult Get()
        {
            return Ok(GetIP());
        }

        protected Info GetIP()
        {
            var info = new Info();
            if (Request.Headers.TryGetValue("X-Forwarded-For", out StringValues forwardeds))
            {
                info.ip = forwardeds.ToString().Split(',').Select(_ => _).ToList();
            }

            if (Request.Headers.TryGetValue("CF-IPCountry", out StringValues country))
            {
                info.country = country;

            }
            return info;
        }
    }

    public class Info
    {
        public List<string> ip { get; set; } = new List<string>();
        public string country { get; set; }
    }
}
