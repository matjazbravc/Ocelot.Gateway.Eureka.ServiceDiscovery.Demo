using Microsoft.AspNetCore.Mvc;

namespace ServiceA.OpenApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return $"Service-A -> 'A' Value";
        }

        [HttpGet("status")]
        public string Status()
        {
            return $"Service-A, running on {Request.Host}";
        }

        [HttpGet("healthcheck")]
        public IActionResult Healthcheck()
        {
            return Ok("Service-A is healthy");
        }
    }
}
