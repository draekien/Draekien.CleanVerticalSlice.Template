using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Interfaces;
using WeatherForecast.Infrastructure.ApiClients;

namespace WeatherForecast.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IWeatherForecastApiClient, MockWeatherForecastApiClient>();
        }
    }
}
