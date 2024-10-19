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
                info.ip_raw = forwardeds.ToString().Split(',').Select(_ => _).First();
            }
            if (Request.Headers.TryGetValue("Cf-Pseudo-IPv4", out StringValues ipv4))
            {
                info.pseudo_IPv4 = forwardeds.ToString().Split(',').Select(_ => _).First();
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
        public string ip_raw { get; set; }
        public string pseudo_IPv4 { get; set; }
        public string country { get; set; }
    }
}
