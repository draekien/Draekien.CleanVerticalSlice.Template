using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Draekien.CleanVerticalSlice.Common.Application.Mappings;
using Draekien.CleanVerticalSlice.Common.TestUtils.Mappings;
using FluentAssertions;
using MediatR;
using WeatherForecast.Application.Commands.SubmitWeatherForecast;
using WeatherForecast.Application.Interfaces;
using WeatherForecast.Infrastructure.ApiClients;
using Xunit;

namespace WeatherForecast.Application.UnitTests.Commands.SubmitWeatherForecast
{
    public class SubmitWeatherForecastHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly IRequestHandler<SubmitWeatherForecastCommand, bool> _sut;

        public SubmitWeatherForecastHandlerTests()
        {
            _fixture = new Fixture();

            IWeatherForecastApiClient weatherForecastApiClient = new MockWeatherForecastApiClient();
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperTestingProfile(typeof(DependencyInjection).Assembly))));

            _sut = new SubmitWeatherForecastCommand.Handler(mapper, weatherForecastApiClient);
        }

        [Fact]
        public async Task GivenValidSubmitWeatherForecastCommand_WhenHandlingCommand_ThenReturnTrue()
        {
            // arrange
            var cmd = _fixture.Build<SubmitWeatherForecastCommand>()
                              .With(x => x.LocalDateTime, DateTime.Now)
                              .Create();

            // act
            var result = await _sut.Handle(cmd, default);

            // assert
            result.Should()
                  .BeTrue();
        }
    }
}
