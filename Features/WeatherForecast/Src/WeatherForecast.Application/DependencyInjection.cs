using Common.Application;

using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddCommonApplication();
        }
    }
}
