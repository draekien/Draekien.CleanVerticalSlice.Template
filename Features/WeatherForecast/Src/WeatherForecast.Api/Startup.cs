using Draekien.CleanVerticalSlice.Common.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecast.Application;
using WeatherForecast.Infrastructure;

namespace WeatherForecast.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment HostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCommonApi(
                HostEnvironment,
                typeof(Startup).Namespace,
                new[]
                {
                    typeof(Startup).Assembly,
                    typeof(Application.DependencyInjection).Assembly
                });

            services.AddApplication();
            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCommonApi(env, $"{typeof(Startup).Namespace} v1");
        }
    }
}
