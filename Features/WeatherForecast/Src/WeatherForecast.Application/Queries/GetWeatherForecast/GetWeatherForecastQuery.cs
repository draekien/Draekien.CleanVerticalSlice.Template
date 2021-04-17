using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using WeatherForecast.Application.Entities;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Application.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQuery : IRequest<IEnumerable<ForecastVm>>
    {
        /// <summary>The number of days to forecast</summary>
        public int Days { get; set; }

        public class Validator : AbstractValidator<GetWeatherForecastQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Days)
                    .GreaterThan(0);
            }
        }

        public class Handler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<ForecastVm>>
        {
            private readonly IMapper _mapper;
            private readonly IWeatherForecastApiClient _weatherForecastApiClient;

            public Handler(IWeatherForecastApiClient weatherForecastApiClient, IMapper mapper)
            {
                _weatherForecastApiClient = weatherForecastApiClient ?? throw new ArgumentNullException(nameof(weatherForecastApiClient));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            /// <inheritdoc />
            public async Task<IEnumerable<ForecastVm>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Forecast> forecasts = await _weatherForecastApiClient.GetForecastAsync(request.Days, cancellationToken);
                var result = _mapper.Map<IEnumerable<ForecastVm>>(forecasts);

                return result;
            }
        }
    }
}
