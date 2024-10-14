using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;

namespace Services.Gateway;

public class Startup(IConfiguration configuration)
{
  private const string SERVICE_NAME = "Services.Gateway";

  public IConfiguration Configuration { get; } = configuration;

  public static void ConfigureServices(IServiceCollection services)
  {
    services.AddCors();
    services.AddOcelot()
      .AddEureka() // https://ocelot.readthedocs.io/en/latest/features/servicediscovery.html#eureka
      .AddPolly()
      .AddCacheManager(x =>
      {
        x.WithDictionaryHandle();
      });
  }

  public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
  {
    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }

    app.UseCors(b => b
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader()
    );

    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
      endpoints.MapGet("/", async context =>
      {
        await context.Response.WriteAsync(SERVICE_NAME);
      });
      endpoints.MapGet("/info", async context =>
      {
        await context.Response.WriteAsync($"{SERVICE_NAME}, running on {context.Request.Host}");
      });
    });

    app.UseOcelot().GetAwaiter().GetResult();
  }
}
