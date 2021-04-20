using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.TestHelper;
using WeatherForecast.Application.Commands.SubmitWeatherForecast;
using Xunit;

namespace WeatherForecast.Application.UnitTests.Commands.SubmitWeatherForecast
{
    public class SubmitWeatherForecastValidatorTests
    {
        private readonly IValidator<SubmitWeatherForecastCommand> _sut;

        public SubmitWeatherForecastValidatorTests()
        {
            _sut = new SubmitWeatherForecastCommand.Validator();
        }

        [Fact]
        public async Task GivenValidSubmitWeatherCommand_WhenValidatingCommand_ThenReturnNoFailures()
        {
            // arrange
            var command = new SubmitWeatherForecastCommand
            {
                LocalDateTime = DateTime.Now,
                Summary = "summary",
                TemperatureC = 20
            };

            // act
            var result = await _sut.TestValidateAsync(command);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.Summary);
            result.ShouldNotHaveValidationErrorFor(x => x.LocalDateTime);
            result.ShouldNotHaveValidationErrorFor(x => x.TemperatureC);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(2020,1,1)]
        public async Task GivenInvalidLocalDateTime_WhenValidatingCommand_ThenReturnFailureForLocalDateTime(int? year, int? month, int? day)
        {
            // arrange
            DateTime? date = year is null ? null : new DateTime(year.Value, month!.Value, day!.Value);
            var command = new SubmitWeatherForecastCommand
            {
                Summary = "summary",
                TemperatureC = 20,
            };

            if (date is not null) command.LocalDateTime = date.Value;

            // act
            var result = await _sut.TestValidateAsync(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.LocalDateTime);
            result.ShouldNotHaveValidationErrorFor(x => x.Summary);
            result.ShouldNotHaveValidationErrorFor(x => x.TemperatureC);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("something that is definitely over fifty characters blah blah blah blah blah")]
        public async Task GivenInvalidSummary_WhenValidatingCommand_ThenReturnFailureForSummary(string summary)
        {
            // arrange
            var command = new SubmitWeatherForecastCommand
            {
                LocalDateTime = DateTime.Now,
                Summary = summary,
                TemperatureC = 20
            };

            // act
            var result = await _sut.TestValidateAsync(command);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Summary);
            result.ShouldNotHaveValidationErrorFor(x => x.LocalDateTime);
            result.ShouldNotHaveValidationErrorFor(x => x.TemperatureC);
        }
    }
}
