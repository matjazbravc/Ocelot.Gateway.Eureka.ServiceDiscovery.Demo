using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace ServiceB.OpenApi;

public class Startup(IConfiguration configuration)
{
  private const string SERVICE_NAME = "ServiceB.OpenApi";

  public IConfiguration Configuration { get; } = configuration;

  public static void ConfigureServices(IServiceCollection services)
  {
    services.AddServiceDiscovery(options => options.UseEureka());
    services.AddHttpContextAccessor();
    services.AddControllers();
    services.AddCors();
    services.AddRouting(options => options.LowercaseUrls = true);
  }

  public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseHsts();
    }

    app.UseCors(b => b
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()
    );

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
