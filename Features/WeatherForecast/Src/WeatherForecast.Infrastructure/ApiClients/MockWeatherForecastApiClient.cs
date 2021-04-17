using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.Application.Entities;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Infrastructure.ApiClients
{
    public class MockWeatherForecastApiClient : IWeatherForecastApiClient
    {
        private static readonly string[] Summaries =
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        /// <inheritdoc />
        public async Task<IEnumerable<Forecast>> GetForecastAsync(int days, CancellationToken cancellationToken)
        {
            var rng = new Random();
            IEnumerable<Forecast> result = Enumerable.Range(1, days)
                                                     .Select(
                                                         index => new Forecast
                                                         {
                                                             DateTimeOffset = DateTimeOffset.Now.AddDays(index),
                                                             TemperatureC = rng.Next(-20, 55),
                                                             Summary = Summaries[rng.Next(Summaries.Length)]
                                                         }
                                                     );

            return await Task.FromResult(result);
        }
    }
}
