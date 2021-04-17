using System.Threading.Tasks;
using AutoFixture;
using FluentValidation;
using FluentValidation.TestHelper;
using WeatherForecast.Application.Queries.GetWeatherForecast;
using Xunit;

namespace WeatherForecast.Application.UnitTests.Queries.GetWeatherForecast
{
    public class GetWeatherForecastQueryValidatorTests
    {
        private readonly IFixture _fixture;
        private readonly IValidator<GetWeatherForecastQuery> _sut;

        public GetWeatherForecastQueryValidatorTests()
        {
            _fixture = new Fixture();
            _sut = new GetWeatherForecastQuery.Validator();
        }

        [Fact]
        public async Task GivenDaysLessThanOrEqualToZero_WhenValidatingQuery_ThenShouldHaveValidationErrorForDays()
        {
            // arrange
            GetWeatherForecastQuery query = _fixture.Build<GetWeatherForecastQuery>()
                                                    .With(x => x.Days, 0)
                                                    .Create();

            // act
            TestValidationResult<GetWeatherForecastQuery> result = await _sut.TestValidateAsync(query);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Days);
        }

        [Fact]
        public async Task GivenDaysGreaterThanZero_WhenValidationQuery_ThenShouldNotHaveValidationErrorForDays()
        {
            // arrange
            GetWeatherForecastQuery query = _fixture.Build<GetWeatherForecastQuery>()
                                                    .With(x => x.Days, 1)
                                                    .Create();

            // act
            TestValidationResult<GetWeatherForecastQuery> result = await _sut.TestValidateAsync(query);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.Days);
        }

        [Fact]
        public async Task GivenNullDays_WhenValidatingQuery_ThenShouldHaveValidationErrorForDays()
        {
            // arrange
            GetWeatherForecastQuery query = new();

            // act
            TestValidationResult<GetWeatherForecastQuery> result = await _sut.TestValidateAsync(query);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Days);
        }
    }
}
