using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Draekien.CleanVerticalSlice.Common.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Commands.SubmitWeatherForecast;
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

        /// <summary>
        /// Submits a weather forecast for a day in the future
        /// </summary>
        /// <param name="command">The forecast to submit</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>True on success</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> PostAsync([FromBody] SubmitWeatherForecastCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);

            return Accepted();
        }
    }
}
