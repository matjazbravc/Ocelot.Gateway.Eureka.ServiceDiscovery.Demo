using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace ServiceA.OpenApi;

public class Startup(IConfiguration configuration)
{
  private const string SERVICE_NAME = "ServiceA.OpenApi";

  public IConfiguration Configuration { get; } = configuration;

  public static void ConfigureServices(IServiceCollection services)
  {
    services.AddServiceDiscovery(options => options.UseEureka());
    services.AddHttpContextAccessor();
    services.AddControllers();
    services.AddRouting();
  }

  public static void Configure(IApplicationBuilder app)
  {
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
      endpoints.MapGet("/", async context =>
      {
        await context.Response.WriteAsync(SERVICE_NAME);
      });
    });
  }
}
