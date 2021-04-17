using System;
using Draekien.CleanVerticalSlice.Common.Api.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WeatherForecast.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Log.Logger = SerilogConfiguration.CreateLogger();

                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly, check the application's WebHost configuration");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            return -1;
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .UseSerilog()
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
