using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using Draekien.CleanVerticalSlice.Common.Application.Mappings;
using FluentAssertions;
using MediatR;
using WeatherForecast.Application.Interfaces;
using WeatherForecast.Application.Queries.GetWeatherForecast;
using WeatherForecast.Infrastructure.ApiClients;
using Xunit;

namespace WeatherForecast.Application.UnitTests.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQueryHandlerTests
    {
        private readonly IRequestHandler<GetWeatherForecastQuery, IEnumerable<ForecastVm>> _sut;

        public GetWeatherForecastQueryHandlerTests()
        {
            IWeatherForecastApiClient weatherForecastApiClient = new MockWeatherForecastApiClient();
            IMapper mapper = new Mapper(new MapperConfiguration(config => { config.AddProfile(new MappingProfile(typeof(DependencyInjection).Assembly)); }));

            _sut = new GetWeatherForecastQuery.Handler(weatherForecastApiClient, mapper);
        }

        [Theory] [InlineAutoData]
        public async Task GivenSomeNumberOfDaysGreaterThanZero_WhenHandlingQuery_ThenReturnThatManyDaysOfForecasts(int days)
        {
            // arrange
            if (days <= 0) days = 1;
            GetWeatherForecastQuery query = new()
            {
                Days = days
            };

            // act
            IEnumerable<ForecastVm> result = await _sut.Handle(query, default);

            // assert
            result.Count()
                  .Should()
                  .Be(days);
        }
    }
}
