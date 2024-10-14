using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Services.Gateway;

public static class Program
{
  public static async Task Main(string[] args)
  {
    await CreateHostBuilder(args).Build().RunAsync();
  }

  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      })
      .ConfigureAppConfiguration((hostingContext, config) =>
      {
        config
          .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
          .AddEnvironmentVariables()
          .AddJsonFile("ocelot.json", optional: false)
          .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
      })
      .ConfigureLogging((builderContext, logging) =>
      {
        logging.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddEventSourceLogger();
      });
}