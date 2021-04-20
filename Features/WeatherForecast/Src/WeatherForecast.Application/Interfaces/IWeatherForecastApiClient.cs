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
        /// <param name="days">The number of days</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<IEnumerable<Forecast>> GetForecastAsync(int days, CancellationToken cancellationToken);

        /// <summary>
        /// Submit a weather forecast reading
        /// </summary>
        /// <param name="forecast">The <see cref="Forecast"/> being submitted</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>True on success</returns>
        Task<bool> SubmitForecastAsync(Forecast forecast, CancellationToken cancellationToken);
    }
}
