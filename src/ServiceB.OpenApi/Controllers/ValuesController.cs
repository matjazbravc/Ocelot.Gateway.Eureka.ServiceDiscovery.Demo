using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace ServiceB.OpenApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
  private readonly ILogger<ValuesController> _logger;
  private readonly string _baseUrl;
  private readonly HttpRequest _httpRequest;
  private readonly string _service_name;

  public ValuesController(ILogger<ValuesController> logger, IHttpContextAccessor context)
  {
    _logger = logger;
    if (context.HttpContext != null)
    {
      _httpRequest = context.HttpContext.Request;
      _baseUrl = $"{_httpRequest.Scheme}://{_httpRequest.Host}";
    }
    _service_name = Assembly.GetExecutingAssembly().GetName().Name;
  }

  [HttpGet("badcode")]
  public string BadCode() => throw new ArgumentException("Some bad code was executed!");

  [HttpGet]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Get()
  {
    string msg = $"{_service_name}, Url: {_baseUrl}, Method: {_httpRequest.Method}, Path: {_httpRequest.Path} -> Value";
    _logger.LogInformation("{Message}", msg);
    return Ok(msg);
  }

  [HttpGet("healthcheck")]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Healthcheck()
  {
    string msg = $"{_httpRequest.Host} is healthy";
    _logger.LogInformation("{Message}", msg);
    return Ok(msg);
  }

  [HttpGet("status")]
  [ProducesResponseType<string>(StatusCodes.Status200OK)]
  public IActionResult Status()
  {
    string msg = $"Running on {_httpRequest.Host}";
    _logger.LogInformation("{Message}", msg);
    return Ok(msg);
  }
}
