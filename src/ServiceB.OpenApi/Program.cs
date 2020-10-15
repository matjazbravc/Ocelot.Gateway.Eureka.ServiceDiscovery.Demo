using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ServiceB.OpenApi;

public static class Program
{
  public async static Task Main(string[] args)
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
          .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", false, true);
      })
      .ConfigureLogging((builderContext, logging) =>
      {
        logging.AddConfiguration(builderContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddEventSourceLogger();
      });
}