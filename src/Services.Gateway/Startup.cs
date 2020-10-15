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
using Steeltoe.Discovery.Client;

namespace Services.Gateway
{
    public class Startup
    {
        private const string SERVICE_NAME = "Services.Gateway";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddOcelot()
                .AddEureka()
                .AddPolly()
                .AddCacheManager(x =>
                {
                    x.WithDictionaryHandle();
                });
            services.AddDiscoveryClient(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles();
            app.UseDiscoveryClient();
            app.UseEndpoints(config =>
            {
                config.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(SERVICE_NAME);
                });
                config.MapGet("/info", async context =>
                {
                    await context.Response.WriteAsync($"{SERVICE_NAME}, running on {context.Request.Host}");
                });
            });
            app.UseOcelot().Wait();
        }
    }
}
