using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Application.Commands.SubmitWeatherForecast
{
    public class SubmitWeatherForecastCommand : IRequest<bool>
    {
        /// <summary>
        /// The local date time of the forecast
        /// </summary>
        public DateTime LocalDateTime { get; set; }
        /// <summary>
        /// The forecast summary
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// The temperature in Celsius
        /// </summary>
        public int TemperatureC { get; set; }

        public class Validator : AbstractValidator<SubmitWeatherForecastCommand>
        {
            public Validator()
            {
                RuleFor(x => x.LocalDateTime)
                    .NotNull()
                    .GreaterThanOrEqualTo(DateTime.Today);

                RuleFor(x => x.Summary)
                    .NotEmpty()
                    .MaximumLength(50);

                RuleFor(x => x.TemperatureC)
                    .NotNull();
            }
        }

        public class Handler : IRequestHandler<SubmitWeatherForecastCommand, bool>
        {
            private readonly IMapper _mapper;
            private readonly IWeatherForecastApiClient _weatherForecastApiClient;

            public Handler(IMapper mapper, IWeatherForecastApiClient weatherForecastApiClient)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _weatherForecastApiClient = weatherForecastApiClient ?? throw new ArgumentNullException(nameof(weatherForecastApiClient));
            }

            /// <inheritdoc />
            public async Task<bool> Handle(SubmitWeatherForecastCommand request, CancellationToken cancellationToken)
            {
                var forecastToSubmit = _mapper.Map<ForecastDto>(request);
                var result = await _weatherForecastApiClient.SubmitForecastAsync(forecastToSubmit, cancellationToken);

                return result;
            }
        }
    }
}
