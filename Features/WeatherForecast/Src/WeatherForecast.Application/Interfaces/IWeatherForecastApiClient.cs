using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Application.Entities;

namespace WeatherForecast.Application.Interfaces
{
    public interface IWeatherForecastApiClient
    {
        /// <summary>
        /// Gets the weather forecast for the next X days
        /// </summary>
        /// <param name="days"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Forecast>> GetForecastAsync(int days, CancellationToken cancellationToken);
    }
}
