using Microsoft.AspNetCore.Mvc;

namespace ServiceB.OpenApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return $"Service-B -> 'B' Value";
        }

        [HttpGet("status")]
        public string Status()
        {
            return $"Service-B, running on {Request.Host}";
        }

        [HttpGet("healthcheck")]
        public IActionResult Healthcheck()
        {
            return Ok("Service-B is healthy");
        }
    }
}
