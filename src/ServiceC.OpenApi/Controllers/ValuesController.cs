using Microsoft.AspNetCore.Mvc;

namespace ServiceC.OpenApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return $"Service-C -> 'C' Value";
        }

        [HttpGet("status")]
        public string Status()
        {
            return $"Service-C, running on {Request.Host}";
        }

        [HttpGet("healthcheck")]
        public IActionResult Healthcheck()
        {
            return Ok("Service-C is healthy");
        }
    }
}
