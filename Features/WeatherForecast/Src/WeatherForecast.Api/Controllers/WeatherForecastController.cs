using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Draekien.CleanVerticalSlice.Common.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Queries.GetWeatherForecast;

namespace WeatherForecast.Api.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        /// <summary>Gets the weather forecast for the next X days</summary>
        /// <param name="query">The number of days to forecast</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken" /></param>
        /// <returns>A list of weather forecasts for the next X days</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ForecastVm>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetWeatherForecastQuery query, CancellationToken cancellationToken)
        {
            IEnumerable<ForecastVm> result = await Mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
